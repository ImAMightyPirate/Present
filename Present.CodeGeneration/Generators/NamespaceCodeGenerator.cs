// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Generators
{
    using EnsureThat;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Present.CodeGeneration.Constants;
    using Present.CodeGeneration.Contracts;

    /// <summary>
    /// Responsible for generating the Roslyn definition for a namespace.
    /// </summary>
    public class NamespaceCodeGenerator : INamespaceCodeGenerator
    {
        private readonly IXmlHeaderGenerator xmlHeaderGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceCodeGenerator"/> class.
        /// </summary>
        /// <param name="xmlHeaderGenerator">The XML header generator.</param>
        public NamespaceCodeGenerator(
            IXmlHeaderGenerator xmlHeaderGenerator)
        {
            this.xmlHeaderGenerator = xmlHeaderGenerator;
        }

        /// <summary>
        /// Generates a Roslyn namespace definition.
        /// </summary>
        /// <param name="typeNamespace">The namespace which the type belongs to.</param>
        /// <param name="assemblyQualifiedName">The assembly qualified name of the type being wrapped.</param>
        /// <returns>The generated namespace declaration.</returns>
        public NamespaceDeclarationSyntax Generate(
            string typeNamespace,
            string assemblyQualifiedName)
        {
            Ensure.That(typeNamespace).IsNotNullOrWhiteSpace();
            Ensure.That(assemblyQualifiedName).IsNotNullOrWhiteSpace();

            var xmlHeaderTriviaList = this.xmlHeaderGenerator.Generate(assemblyQualifiedName);

            // The trivia list must be wrapped into a token
            var token = SyntaxFactory.Token(
                xmlHeaderTriviaList,
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
