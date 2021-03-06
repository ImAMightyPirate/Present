﻿// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration
{
    using EnsureThat;
    using Ninject.Extensions.Logging;
    using Present.CodeGeneration.Contracts;
    using Present.CodeGeneration.Wrappers.Custom;

    /// <summary>
    /// Class responsible for determining whether a method can have a wrapper automatically
    /// generated.
    /// </summary>
    public class MethodAnalyser : IMethodAnalyser
    {
        private readonly ILogger logger;
        private readonly ITypeAnalyser typeAnalyser;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodAnalyser"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="typeAnalyser">The type analyser.</param>
        public MethodAnalyser(
            ILogger logger,
            ITypeAnalyser typeAnalyser)
        {
            this.logger = logger;
            this.typeAnalyser = typeAnalyser;
        }

        /// <summary>
        /// Determines whether a method can be wrapped automatically
        /// by the Present code generator.
        /// </summary>
        /// <param name="method">The method to analyse.</param>
        /// <returns>Returns true when the method can be wrapped automatially, otherwise false.</returns>
        public bool CanWrap(IMethodInfoWrapper method)
        {
            Ensure.That(method).IsNotNull();

            // The method must be static and not per instance
            if (!method.IsStatic)
            {
                this.logger.Debug($"Method '{method.Name}' is not static");
                return false;
            }

            // Presence of special name flag indicates something out of the ordinary
            // (such as an operator which cannot be wrapped)
            if (method.IsSpecialName)
            {
                this.logger.Debug($"Method '{method.Name}' has special name");
                return false;
            }

            // Return type must be a supported type
            if (!this.typeAnalyser.CanWrap(method.ReturnType))
            {
                this.logger.Debug($"Method '{method.Name}' return type is unsupported ('{method.ReturnType.Name}')");
                return false;
            }

            // All parameters of the method have a supported type
            // (could be simpified by using Linq but is easier to debug in this format)
            var parameters = method.GetParameters();

            foreach (var parameter in parameters)
            {
                if (!this.typeAnalyser.CanWrap(parameter.ParameterType))
                {
                    this.logger.Debug($"Method '{method.Name}' parameter '{parameter.Name}' has unsupported type ('{parameter.ParameterType.Name}')");
                    return false;
                }
            }

            // Types are not simple enough for us to be able to easily wrap
            return true;
        }
    }
}