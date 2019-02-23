namespace Present.CodeGeneration
{
    using System;
    using System.Reflection;
    using Contracts;
    using EnsureThat;
    using Ninject.Extensions.Logging;

    /// <summary>
    /// Class responsible for determining whether a method can have a wrapper automatically
    /// generated.
    /// </summary>
    public class MethodAnalyser : IMethodAnalyser
    {
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodAnalyser"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public MethodAnalyser(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Determines whether a method can be wrapped automatically
        /// by the Present code generator.
        /// </summary>
        /// <param name="method">The method to analyse.</param>
        /// <returns>Returns true when the method can be wrapped automatially, otherwise false.</returns>
        public bool IsWrappingSupported(MethodInfo method)
        {
            Ensure.That(method).IsNotNull();

            // The method must be static and not per instance
            if (!method.IsStatic)
            {
                this.logger.Debug($"Method '{method.Name}' is not static");
                return false;
            }

            // Return type must be a supported type
            if (!this.IsSupportedType(method.ReturnType))
            {
                this.logger.Debug($"Method '{method.Name}' return type is unsupported ('{method.ReturnType.Name}')");
                return false;
            }

            // All parameters of the method have a supported type
            // (could be simpified by using Linq but is easier to debug in this format)
            var parameters = method.GetParameters();

            foreach (var parameter in parameters)
            {
                if (!this.IsSupportedType(parameter.ParameterType))
                {
                    this.logger.Debug($"Method '{method.Name}' parameter '{parameter.Name}' has unsupported type ('{parameter.ParameterType.Name}')");
                    return false;
                }
            }

            // Types are not simple enough for us to be able to easily wrap
            return true;
        }

        private bool IsSupportedType(Type type)
        {
            // If an array then check the type of the element
            if (type.IsArray)
            {
                return this.IsSupportedType(type.GetElementType());
            }

            // Common reference types that cause no difficulties with mocking are supported
            if (type == typeof(string))
            {
                return true;
            }

            // Structures passed by reference (ref struct), such as Span<T>, are not supported
            // even though they are value types
            if (type.IsByRefLike)
            {
                return false;
            }

            // Any remaining value types are supported (which also includes the void return type)
            return type.IsValueType;
        }
    }
}