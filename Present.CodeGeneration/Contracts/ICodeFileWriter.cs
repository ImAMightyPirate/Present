namespace Present.CodeGeneration.Contracts
{
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Contract for the code file writer.
    /// </summary>
    public interface ICodeFileWriter
    {
        /// <summary>
        /// Writes the namespace declaration to a code file in the output path.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="namespaceDeclaration">The namespace declaration.</param>
        /// <param name="outputPath">The output path for the file.</param>
        void WriteCodeFileToPath(
            string typeName,
            NamespaceDeclarationSyntax namespaceDeclaration,
            string outputPath);
    }
}