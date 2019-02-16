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