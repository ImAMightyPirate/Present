// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Generators
{
    using System.Collections.Generic;
    using System.Reflection;
    using EnsureThat;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Ninject.Extensions.Logging;
    using Present.CodeGeneration.Constants;
    using Present.CodeGeneration.Contracts;

    /// <summary>
    /// Responsible for generating the Roslyn definition for a namespace.
    /// </summary>
    public class NamespaceCodeGenerator : INamespaceCodeGenerator
    {
        private readonly ICopyrightXmlGenerator copyrightXmlGenerator;
        private readonly IInterfaceCodeGenerator interfaceCodeGenerator;
        private readonly IClassCodeGenerator classCodeGenerator;
        private readonly IMethodCodeGenerator methodCodeGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceCodeGenerator"/> class.
        /// </summary>
        /// <param name="copyrightXmlGenerator">The copyright XML generator.</param>
        /// <param name="interfaceCodeGenerator">The interface code generator.</param>
        /// <param name="classCodeGenerator">The class code generator.</param>
        /// <param name="methodCodeGenerator">The method code generator.</param>
        public NamespaceCodeGenerator(
            ICopyrightXmlGenerator copyrightXmlGenerator,
            IInterfaceCodeGenerator interfaceCodeGenerator,
            IClassCodeGenerator classCodeGenerator,
            IMethodCodeGenerator methodCodeGenerator)
        {
            this.copyrightXmlGenerator = copyrightXmlGenerator;
            this.interfaceCodeGenerator = interfaceCodeGenerator;
            this.classCodeGenerator = classCodeGenerator;
            this.methodCodeGenerator = methodCodeGenerator;
        }

        /// <summary>
        /// Generates a Roslyn namespace definition from a type and its methods.
        /// </summary>
        /// <param name="typeNamespace">The namespace which the type belongs to.</param>
        /// <param name="typeName">The name of the type.</param>
        /// <param name="methods">The methods to be wrapped.</param>
        /// <returns>The generated namespace declaration.</returns>
        public NamespaceDeclarationSyntax Generate(string typeNamespace, string typeName, IEnumerable<MethodInfo> methods)
        {
            Ensure.That(typeNamespace).IsNotNullOrWhiteSpace();
            Ensure.That(typeName).IsNotNullOrWhiteSpace();
            Ensure.That(methods).IsNotNull();

            // Add a prefix to create an interface name from the type name
            var interfaceName = $"{Interface.DefaultPrefix}{typeName}";

            var interfaceDeclaration = this.interfaceCodeGenerator.Generate(interfaceName);
            var classDeclaration = this.classCodeGenerator.Generate(typeName, interfaceName);

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

            var namespaceDeclaration = this.CreateNamespace(typeNamespace);

            // Add the interface to the namespace
            namespaceDeclaration = namespaceDeclaration.AddMembers(interfaceDeclaration);

            // Add the class to the namespace
            namespaceDeclaration = namespaceDeclaration.AddMembers(classDeclaration);

            return namespaceDeclaration;
        }

        private NamespaceDeclarationSyntax CreateNamespace(string typeNamespace)
        {
            var documentationCommentTrivia = this.copyrightXmlGenerator.Generate();

            // The documentation comment trivia must be wrapped into a token
            var token = SyntaxFactory.Token(
                SyntaxFactory.TriviaList(SyntaxFactory.Trivia(documentationCommentTrivia)),
                SyntaxKind.NamespaceKeyword,
                SyntaxFactory.TriviaList());

            // Substitute the System namespace with a Present equivelant
            var namespaceName = SyntaxFactory.ParseName(typeNamespace.Replace(Namespace.System, Namespace.Present));

            var namespaceDeclaration = SyntaxFactory
                .NamespaceDeclaration(namespaceName)
                .WithNamespaceKeyword(token);

            return namespaceDeclaration;
        }
    }
}
