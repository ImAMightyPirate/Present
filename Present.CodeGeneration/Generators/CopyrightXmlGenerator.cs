﻿namespace Present.CodeGeneration.Generators
{
    using System;
    using Constants;
    using Contracts;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Responsible for generating the Roslyn definition for an XML copyright header.
    /// </summary>
    public class CopyrightXmlGenerator : ICopyrightXmlGenerator
    {
        /// <summary>
        /// Generates a Roslyn comment trivia for the XML copyright header.
        /// </summary>
        /// <returns>The generated XML copyright header.</returns>
        public DocumentationCommentTriviaSyntax Generate()
        {
            var documentationCommentTrivia = SyntaxFactory.DocumentationCommentTrivia(
                SyntaxKind.SingleLineDocumentationCommentTrivia,
                SyntaxFactory.List(
                    new XmlNodeSyntax[]
                    {
                        SyntaxFactory.XmlText().WithTextTokens(SyntaxFactory.TokenList(this.GetDocumentationCommentToken(string.Empty))),
                        this.GetDocumentationXmlElement(),
                        SyntaxFactory.XmlText().WithTextTokens(SyntaxFactory.TokenList(this.GetNewLineToken())),
                        SyntaxFactory.XmlText().WithTextTokens(SyntaxFactory.TokenList(this.GetNewLineToken()))
                    }));

            return documentationCommentTrivia;
        }

        private XmlElementSyntax GetDocumentationXmlElement()
        {
            return SyntaxFactory.XmlExampleElement(
                    SyntaxFactory.SingletonList<XmlNodeSyntax>(
                        SyntaxFactory.XmlText().WithTextTokens(
                            SyntaxFactory.TokenList(
                                new[]
                                {
                                    this.GetNewLineToken(),
                                    this.GetDocumentationCommentToken(Resource.AutoGenerationDisclaimer),
                                    this.GetNewLineToken(),
                                    this.GetDocumentationCommentToken(Resource.ProjectWebsite),
                                    this.GetNewLineToken(),
                                    this.GetDocumentationCommentToken(Resource.LicenceDeclaration),
                                    this.GetNewLineToken(),
                                    this.GetDocumentationCommentToken(string.Empty)
                                }))))
                .WithStartTag(SyntaxFactory.XmlElementStartTag(SyntaxFactory.XmlName(SyntaxFactory.Identifier(XmlElementIdentifier.Copyright))))
                .WithEndTag(SyntaxFactory.XmlElementEndTag(SyntaxFactory.XmlName(SyntaxFactory.Identifier(XmlElementIdentifier.Copyright))));
        }

        private SyntaxToken GetNewLineToken()
        {
            return SyntaxFactory.XmlTextNewLine(
                SyntaxFactory.TriviaList(),
                Environment.NewLine,
                Environment.NewLine,
                SyntaxFactory.TriviaList());
        }

        private SyntaxToken GetDocumentationCommentToken(string commentText)
        {
            return SyntaxFactory.XmlTextLiteral(
                SyntaxFactory.TriviaList(SyntaxFactory.DocumentationCommentExterior(Comment.SingleLineDocumentationPrefix)),
                $" {commentText}",
                $" {commentText}",
                SyntaxFactory.TriviaList());
        }
    }
}