// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Contracts
{
    using Present.CodeGeneration.Wrappers.Custom;

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
            INamespaceDeclarationSyntaxWrapper namespaceDeclaration,
            string outputPath);
    }
}