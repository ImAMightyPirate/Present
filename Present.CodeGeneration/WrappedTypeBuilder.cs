// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration
{
    using System.Collections.Generic;
    using System.Reflection;
    using EnsureThat;
    using Ninject.Extensions.Logging;
    using Present.CodeGeneration.Contracts;
    using Present.CodeGeneration.Wrappers.Custom;

    /// <summary>
    /// Class responsible from building a wrapped type.
    /// </summary>
    public class WrappedTypeBuilder : IWrappedTypeBuilder
    {
        private readonly ILogger logger;
        private readonly IType type;
        private readonly IMethodAnalyser methodAnalyser;
        private readonly IWrapperGenerator wrapperGenerator;
        private readonly ICodeFileWriter codeFileWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="WrappedTypeBuilder"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="type">Wrapper for <see cref="System.Type"/>.</param>
        /// <param name="methodAnalyser">The method analyser.</param>
        /// <param name="wrapperGenerator">The wrapper generator.</param>
        /// <param name="codeFileWriter">The code file writer.</param>
        public WrappedTypeBuilder(
            ILogger logger,
            IType type,
            IMethodAnalyser methodAnalyser,
            IWrapperGenerator wrapperGenerator,
            ICodeFileWriter codeFileWriter)
        {
            this.logger = logger;
            this.type = type;
            this.methodAnalyser = methodAnalyser;
            this.wrapperGenerator = wrapperGenerator;
            this.codeFileWriter = codeFileWriter;
        }

        /// <summary>
        /// Creates a wrapper class for a .NET type and outputs
        /// the code file.
        /// </summary>
        /// <param name="options">The program options.</param>
        public void Wrap(IWrapOptions options)
        {
            Ensure.That(options).IsNotNull();
            Ensure.That(options.AssemblyQualifiedTypeNames).IsNotNull();
            Ensure.That(options.OutputPath).IsNotNullOrWhiteSpace();

            foreach (var assemblyQualifiedTypeName in options.AssemblyQualifiedTypeNames)
            {
                var resolvedType = this.type.GetType(assemblyQualifiedTypeName);

                if (resolvedType == null)
                {
                    this.logger.Error($"Type not found for assembly qualified name '{assemblyQualifiedTypeName}'. Wrapper cannot be generated.");
                    continue;
                }

                var supportedMethods = new List<MethodInfo>();

                var totalMethodCount = 0;

                foreach (var method in resolvedType.GetMethods())
                {
                    if (this.methodAnalyser.CanWrap(new MethodInfoWrapper(method)))
                    {
                        supportedMethods.Add(method);
                    }

                    totalMethodCount++;
                }

                if (supportedMethods.Count == 0)
                {
                    this.logger.Error(
                        $"No wrapper generated for type '{resolvedType.Name}' as no methods are supported.");

                    continue;
                }

                this.logger.Debug(
                    $"{supportedMethods.Count} of {totalMethodCount} methods for type '{resolvedType.Name}' are supported.");

                var namespaceDeclaration = this.wrapperGenerator.Generate(
                    options,
                    resolvedType.Namespace,
                    resolvedType.Name,
                    supportedMethods);

                this.codeFileWriter.WriteCodeFileToPath(
                    resolvedType.Name,
                    namespaceDeclaration,
                    options.OutputPath);
            }
        }
    }
}
