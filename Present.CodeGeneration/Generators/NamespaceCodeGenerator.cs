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
        private readonly ICopyrightXmlGenerator copyrightXmlGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceCodeGenerator"/> class.
        /// </summary>
        /// <param name="copyrightXmlGenerator">The copyright XML generator.</param>
        public NamespaceCodeGenerator(
            ICopyrightXmlGenerator copyrightXmlGenerator)
        {
            this.copyrightXmlGenerator = copyrightXmlGenerator;
        }

        /// <summary>
        /// Generates a Roslyn namespace definition.
        /// </summary>
        /// <param name="typeNamespace">The namespace which the type belongs to.</param>
        /// <returns>The generated namespace declaration.</returns>
        public NamespaceDeclarationSyntax Generate(string typeNamespace)
        {
            Ensure.That(typeNamespace).IsNotNullOrWhiteSpace();

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
