// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration
{
    using System.Text;
    using EnsureThat;
    using Ninject.Extensions.Logging;
    using Present.CodeGeneration.Constants;
    using Present.CodeGeneration.Contracts;
    using Present.CodeGeneration.Wrappers.Custom;
    using Present.IO;

    /// <summary>
    /// Responsible for writing generated code to a file.
    /// </summary>
    public class CodeFileWriter : ICodeFileWriter
    {
        private readonly ILogger logger;
        private readonly IFile file;
        private readonly IPath path;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeFileWriter"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="file">Wrapper for <see cref="System.IO.File"/>.</param>
        /// <param name="path">Wrapper for <see cref="System.IO.Path"/>.</param>
        public CodeFileWriter(
            ILogger logger,
            IFile file,
            IPath path)
        {
            this.logger = logger;
            this.file = file;
            this.path = path;
        }

        /// <summary>
        /// Writes the namespace declaration to a code file in the output path.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="namespaceDeclaration">The namespace declaration.</param>
        /// <param name="outputPath">The output path for the file.</param>
        public void WriteCodeFileToPath(
            string typeName,
            INamespaceDeclarationSyntaxWrapper namespaceDeclaration,
            string outputPath)
        {
            Ensure.That(typeName).IsNotNullOrWhiteSpace();
            Ensure.That(namespaceDeclaration).IsNotNull();
            Ensure.That(outputPath).IsNotNullOrWhiteSpace();

            // Normalise so that the generated code is in a readable format
            var generatedCode = namespaceDeclaration.ToString();

            var fileName = $"{typeName}.{FileExtension.CSharp}";
            var filePath = this.path.Combine(outputPath, fileName);

            this.logger.Debug($"Writing code file to path '{filePath}'");

            this.file.WriteAllText(filePath, generatedCode, Encoding.UTF8);
        }
    }
}
