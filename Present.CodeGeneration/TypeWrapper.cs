namespace Present.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Contracts;
    using EnsureThat;
    using Ninject.Extensions.Logging;

    /// <summary>
    /// Class responsible from wrapping a type.
    /// </summary>
    public class TypeWrapper : ITypeWrapper
    {
        private readonly ILogger logger;
        private readonly IMethodAnalyser methodAnalyser;
        private readonly INamespaceCodeGenerator namespaceCodeGenerator;
        private readonly ICodeFileWriter codeFileWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeWrapper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="methodAnalyser">The method analyser.</param>
        /// <param name="namespaceCodeGenerator">The namespace code generator.</param>
        /// <param name="codeFileWriter">The code file writer.</param>
        public TypeWrapper(
            ILogger logger,
            IMethodAnalyser methodAnalyser,
            INamespaceCodeGenerator namespaceCodeGenerator,
            ICodeFileWriter codeFileWriter)
        {
            this.logger = logger;
            this.methodAnalyser = methodAnalyser;
            this.namespaceCodeGenerator = namespaceCodeGenerator;
            this.codeFileWriter = codeFileWriter;
        }

        /// <summary>
        /// Creates a wrapper class for a .NET type and outputs
        /// the code file.
        /// </summary>
        /// <param name="assemblyQualifiedTypeName">The assembly qualified name of the .NET type to be wrapped.</param>
        /// <param name="outputPath">The output path of the generated code file.</param>
        public void Wrap(string assemblyQualifiedTypeName, string outputPath)
        {
            Ensure.That(assemblyQualifiedTypeName).IsNotNullOrWhiteSpace();
            Ensure.That(outputPath).IsNotNullOrWhiteSpace();

            var type = Type.GetType(assemblyQualifiedTypeName);

            if (type == null)
            {
                this.logger.Fatal($"Type not found for assembly qualified name '{assemblyQualifiedTypeName}'");
                return;
            }

            var supportedMethods = new List<MethodInfo>();

            var totalMethodCount = 0;

            foreach (var method in type.GetMethods())
            {
                if (this.methodAnalyser.IsWrappingSupported(method))
                {
                    supportedMethods.Add(method);
                }

                totalMethodCount++;
            }

            this.logger.Debug($"{supportedMethods.Count} of {totalMethodCount} methods for type '{type}.Name' are supported.");

            var namespaceDeclaration = this.namespaceCodeGenerator.Generate(
                type.Namespace,
                type.Name,
                supportedMethods);

            this.codeFileWriter.WriteCodeFileToPath(
                type.Name,
                namespaceDeclaration,
                outputPath);
        }
    }
}
