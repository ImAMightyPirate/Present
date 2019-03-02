// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Contracts
{
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Contract for the copyright XML generator.
    /// </summary>
    public interface ICopyrightXmlGenerator
    {
        /// <summary>
        /// Generates a Roslyn comment trivia for the XML copyright header.
        /// </summary>
        /// <returns>The generated XML copyright header.</returns>
        DocumentationCommentTriviaSyntax Generate();
    }
}