// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Wrappers.Custom
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Contract for the <see cref="NamespaceDeclarationSyntax"/> type wrapper.
    /// </summary>
    public interface INamespaceDeclarationSyntaxWrapper
    {
        /// <summary>
        /// Returns the namespace declaration as a normalised string. 
        /// </summary>
        /// <returns>The normalsied string.</returns>
        string ToString();
    }

    /// <summary>
    /// Wrapper for the <see cref="NamespaceDeclarationSyntax"/> type.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class NamespaceDeclarationSyntaxWrapper : INamespaceDeclarationSyntaxWrapper
    {
        /// <summary>
        /// The wrapped <see cref="NamespaceDeclarationSyntax"/> type.
        /// </summary>
        private readonly NamespaceDeclarationSyntax namespaceDeclarationSyntax;

        /// <summary>
        /// Initialises a new instance of the <see cref="NamespaceDeclarationSyntaxWrapper"/> type.
        /// </summary>
        /// <param name="namespaceDeclarationSyntax">The <see cref="NamespaceDeclarationSyntax"/> object to wrap.</param>
        public NamespaceDeclarationSyntaxWrapper(NamespaceDeclarationSyntax namespaceDeclarationSyntax)
        {
            this.namespaceDeclarationSyntax = namespaceDeclarationSyntax;
        }

        /// <summary>
        /// Returns the namespace declaration as a normalised string. 
        /// </summary>
        /// <returns>The normalsied string.</returns>
        public override string ToString()
        {
            return this.namespaceDeclarationSyntax
                .NormalizeWhitespace()
                .ToFullString();
        }
    }
}
