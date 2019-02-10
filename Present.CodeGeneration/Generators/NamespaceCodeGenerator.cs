namespace Present.CodeGeneration.Generators
{
    using System.Collections.Generic;
    using System.Reflection;
    using Constants;
    using Contracts;
    using EnsureThat;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Ninject.Extensions.Logging;

    /// <summary>
    /// Responsible for generating the Roslyn definition for a namespace.
    /// </summary>
    public class NamespaceCodeGenerator : INamespaceCodeGenerator
    {
        private readonly ILogger logger;
        private readonly IInterfaceCodeGenerator interfaceCodeGenerator;
        private readonly IClassCodeGenerator classCodeGenerator;
        private readonly IMethodCodeGenerator methodCodeGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceCodeGenerator"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="interfaceCodeGenerator">The interface code generator.</param>
        /// <param name="classCodeGenerator">The class code generator.</param>
        /// <param name="methodCodeGenerator">The method code generator.</param>
        public NamespaceCodeGenerator(
            ILogger logger,
            IInterfaceCodeGenerator interfaceCodeGenerator,
            IClassCodeGenerator classCodeGenerator,
            IMethodCodeGenerator methodCodeGenerator)
        {
            this.logger = logger;
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

            // Substitute the System namespace with a Present equivelant
            var namespaceName = SyntaxFactory.ParseName(typeNamespace.Replace(Namespace.System, Namespace.Present));

            var namespaceDeclaration = SyntaxFactory.NamespaceDeclaration(namespaceName);
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

            // Add the class to the namespace
            namespaceDeclaration = namespaceDeclaration.AddMembers(interfaceDeclaration);
            namespaceDeclaration = namespaceDeclaration.AddMembers(classDeclaration);

            return namespaceDeclaration;
        }
    }
}
