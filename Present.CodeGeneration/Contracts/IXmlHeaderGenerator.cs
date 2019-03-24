// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Contracts
{
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Contract for the XML header generator.
    /// </summary>
    public interface IXmlHeaderGenerator
    {
        /// <summary>
        /// Generates a Roslyn comment trivia for the XML header.
        /// </summary>
        /// <param name="assemblyQualifiedName">The assembly qualified name of the type being wrapped.</param>
        /// <returns>The generated XML header.</returns>
        DocumentationCommentTriviaSyntax Generate(string assemblyQualifiedName);
    }
}