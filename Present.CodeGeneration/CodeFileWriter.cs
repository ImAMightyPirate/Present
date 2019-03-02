// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration
{
    using System.IO;
    using System.Text;
    using Constants;
    using Contracts;
    using EnsureThat;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Ninject.Extensions.Logging;

    /// <summary>
    /// Responsible for writing generated code to a file.
    /// </summary>
    public class CodeFileWriter : ICodeFileWriter
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeFileWriter"/> class.
        /// </summary>
        /// <param name="logger">Tbe logger.</param>
        public CodeFileWriter(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Writes the namespace declaration to a code file in the output path.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="namespaceDeclaration">The namespace declaration.</param>
        /// <param name="outputPath">The output path for the file.</param>
        public void WriteCodeFileToPath(
            string typeName,
            NamespaceDeclarationSyntax namespaceDeclaration,
            string outputPath)
        {
            Ensure.That(namespaceDeclaration).IsNotNull();
            Ensure.That(outputPath).IsNotNullOrWhiteSpace();

            // Normalise so that the generated code is in a readable format
            var generatedCode = namespaceDeclaration
                .NormalizeWhitespace()
                .ToFullString();

            var fileName = $"{typeName}.{FileExtension.CSharp}";
            var filePath = Path.Combine(outputPath, fileName);

            this.logger.Debug($"Writing code file to path '{filePath}'");

            File.WriteAllText(filePath, generatedCode, Encoding.UTF8);
        }
    }
}
