// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using EnsureThat;
    using Ninject.Extensions.Logging;
    using Present.CodeGeneration.Contracts;

    /// <summary>
    /// Class responsible from wrapping a type.
    /// </summary>
    public class TypeWrapper : ITypeWrapper
    {
        private readonly ILogger logger;
        private readonly IMethodAnalyser methodAnalyser;
        private readonly IWrapperGenerator wrapperGenerator;
        private readonly ICodeFileWriter codeFileWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeWrapper"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="methodAnalyser">The method analyser.</param>
        /// <param name="wrapperGenerator">The wrapper generator.</param>
        /// <param name="codeFileWriter">The code file writer.</param>
        public TypeWrapper(
            ILogger logger,
            IMethodAnalyser methodAnalyser,
            IWrapperGenerator wrapperGenerator,
            ICodeFileWriter codeFileWriter)
        {
            this.logger = logger;
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
            Ensure.That(options.AssemblyQualifiedTypeName).IsNotNullOrWhiteSpace();
            Ensure.That(options.OutputPath).IsNotNullOrWhiteSpace();

            var type = Type.GetType(options.AssemblyQualifiedTypeName);

            if (type == null)
            {
                this.logger.Fatal($"Type not found for assembly qualified name '{options.OutputPath}'");
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

            var namespaceDeclaration = this.wrapperGenerator.Generate(
                options,
                type.Namespace,
                type.Name,
                supportedMethods);

            this.codeFileWriter.WriteCodeFileToPath(
                type.Name,
                namespaceDeclaration,
                options.OutputPath);
        }
    }
}
