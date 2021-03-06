﻿// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Generators
{
    using System.Collections.Generic;
    using System.Linq;
    using EnsureThat;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Present.CodeGeneration.Constants;
    using Present.CodeGeneration.Contracts;
    using Present.CodeGeneration.Wrappers.Custom;

    /// <summary>
    /// Responsible for generating the Roslyn definition for an XML header.
    /// </summary>
    public class XmlHeaderGenerator : IXmlHeaderGenerator
    {
        private readonly IAssemblyWrapper assemblyWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlHeaderGenerator"/> class.
        /// </summary>
        /// <param name="assemblyWrapper">The assembly wrapper.</param>
        public XmlHeaderGenerator(IAssemblyWrapper assemblyWrapper)
        {
            this.assemblyWrapper = assemblyWrapper;
        }

        /// <summary>
        /// Generates a Roslyn comment trivia for the XML copyright header.
        /// </summary>
        /// <param name="assemblyQualifiedName">The assembly qualified name of the type being wrapped.</param>
        /// <returns>The generated XML copyright header.</returns>
        public SyntaxTriviaList Generate(string assemblyQualifiedName)
        {
            Ensure.That(assemblyQualifiedName).IsNotNullOrWhiteSpace();

            var targetFrameworkAttribute = this.assemblyWrapper
                .GetEntryAssemblyTargetFrameworkAttributes();

            var frameworkDescriptor = string.Join("; ", targetFrameworkAttribute.Select(a => a.FrameworkName));

            var comments = new List<SyntaxTrivia>
            {
                this.GetComment($"<{XmlElementIdentifier.AutoGenerated}>"),
                this.GetComment(Resource.AutoGenerationDisclaimer),
                this.GetComment(Resource.ProjectWebsite),
                this.GetComment(Resource.LicenceDeclaration),
                this.GetComment($"{Resource.Type}: {assemblyQualifiedName}"),
                this.GetComment($"{Resource.Framework}: {frameworkDescriptor}"),
                this.GetComment($"</{XmlElementIdentifier.AutoGenerated}>")
            };

            return SyntaxFactory.TriviaList(comments);
        }

        private SyntaxTrivia GetComment(string commentText)
        {
            return SyntaxFactory.Comment($"{Comment.SingleLinePrefix} {commentText}");
        }
    }
}
