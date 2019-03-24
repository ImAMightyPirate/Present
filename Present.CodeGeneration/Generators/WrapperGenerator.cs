// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Generators
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using EnsureThat;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Present.CodeGeneration.Constants;
    using Present.CodeGeneration.Contracts;
    using Present.CodeGeneration.Wrappers.Custom;

    /// <summary>
    /// Responsible for generating the complete Royslyn definition for a wrapper.
    /// </summary>
    public class WrapperGenerator : IWrapperGenerator
    {
        private readonly INamespaceCodeGenerator namespaceCodeGenerator;
        private readonly IAttributeCodeGenerator attributeCodeGenerator;
        private readonly IInterfaceCodeGenerator interfaceCodeGenerator;
        private readonly IClassCodeGenerator classCodeGenerator;
        private readonly IMethodCodeGenerator methodCodeGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="WrapperGenerator"/> class.
        /// </summary>
        /// <param name="namespaceCodeGenerator">The namespace generator.</param>
        /// <param name="attributeCodeGenerator">The attribute code generator.</param>
        /// <param name="interfaceCodeGenerator">The interface code generator.</param>
        /// <param name="classCodeGenerator">The class code generator.</param>
        /// <param name="methodCodeGenerator">The method code generator.</param>
        public WrapperGenerator(
            INamespaceCodeGenerator namespaceCodeGenerator,
            IAttributeCodeGenerator attributeCodeGenerator,
            IInterfaceCodeGenerator interfaceCodeGenerator,
            IClassCodeGenerator classCodeGenerator,
            IMethodCodeGenerator methodCodeGenerator)
        {
            this.namespaceCodeGenerator = namespaceCodeGenerator;
            this.attributeCodeGenerator = attributeCodeGenerator;
            this.interfaceCodeGenerator = interfaceCodeGenerator;
            this.classCodeGenerator = classCodeGenerator;
            this.methodCodeGenerator = methodCodeGenerator;
        }

        /// <summary>
        /// Generates a Roslyn namespace definition from a type and its methods.
        /// </summary>
        /// <param name="options">The program options.</param>
        /// <param name="type">The type being wrapped.</param>
        /// <param name="methods">The methods to be wrapped.</param>
        /// <returns>The generated namespace declaration.</returns>
        public INamespaceDeclarationSyntaxWrapper Generate(
            IWrapOptions options,
            System.Type type,
            IEnumerable<MethodInfo> methods)
        {
            Ensure.That(options).IsNotNull();
            Ensure.That(type).IsNotNull();
            Ensure.That(methods).IsNotNull();

            // Add a prefix to create an interface name from the type name
            var interfaceName = $"{Interface.DefaultPrefix}{type.Name}";

            var attributes = this.CreateAttributes(
                options,
                interfaceName);

            var modifiers = new[]
            {
                SyntaxFactory.Token(SyntaxKind.PublicKeyword),
                SyntaxFactory.Token(SyntaxKind.PartialKeyword)
            };

            var interfaceDeclaration = this.interfaceCodeGenerator.Generate(
                interfaceName,
                modifiers);

            var classDeclaration = this.classCodeGenerator.Generate(
                type.Name,
                attributes,
                modifiers,
                interfaceName);

            // Run generation for each method
            foreach (var method in methods)
            {
                var (methodDeclaration, methodBody) = this.methodCodeGenerator.Generate(method);

                // Add method declaration to interface declaration
                var interfaceMethodDeclaration = methodDeclaration
                    .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken));

                interfaceDeclaration = interfaceDeclaration.AddMembers(interfaceMethodDeclaration);

                // Add method declaration and body to class definition
                var classMethodDeclaration = methodDeclaration
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .WithBody(methodBody);

                classDeclaration = classDeclaration.AddMembers(classMethodDeclaration);
            }

            var namespaceDeclaration = this.namespaceCodeGenerator.Generate(
                type.Namespace,
                type.AssemblyQualifiedName);

            // Add the interface to the namespace
            namespaceDeclaration = namespaceDeclaration.AddMembers(interfaceDeclaration);

            // Add the class to the namespace
            namespaceDeclaration = namespaceDeclaration.AddMembers(classDeclaration);

            return new NamespaceDeclarationSyntaxWrapper(namespaceDeclaration);
        }

        private AttributeSyntax[] CreateAttributes(
            IWrapOptions options,
            string interfaceName)
        {
            var toolNameArgument = SyntaxFactory.AttributeArgument(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    SyntaxFactory.Literal(Resource.ProjectName)));

            var toolVersionArgument = SyntaxFactory.AttributeArgument(
                SyntaxFactory.LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    SyntaxFactory.Literal(string.Empty)));

            var attributes = new List<AttributeSyntax>
            {
                this.attributeCodeGenerator.Generate(typeof(DebuggerNonUserCodeAttribute)),
                this.attributeCodeGenerator.Generate(typeof(ExcludeFromCodeCoverageAttribute)),
                this.attributeCodeGenerator.Generate(typeof(GeneratedCodeAttribute), toolNameArgument, toolVersionArgument)
            };

            var mefExportArgument = SyntaxFactory.AttributeArgument(
                SyntaxFactory.TypeOfExpression(
                    SyntaxFactory.IdentifierName(interfaceName)));

            if (options.IncludeMefAttribute)
            {
                attributes.Add(this.attributeCodeGenerator.Generate(Attribute.MefExport, mefExportArgument));
            }

            if (options.IncludeMef2Attribute)
            {
                attributes.Add(this.attributeCodeGenerator.Generate(Attribute.Mef2Export, mefExportArgument));
            }

            return attributes.ToArray();
        }
    }
}
