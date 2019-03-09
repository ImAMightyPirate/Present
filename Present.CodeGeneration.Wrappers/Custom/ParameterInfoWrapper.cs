// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Wrappers.Custom
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    /// <summary>
    /// Contract for the <see cref="ParameterInfo"/> type wrapper.
    /// </summary>
    public interface IParameterInfoWrapper
    {
        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the type of the parameter.
        /// </summary>
        ITypeWrapper ParameterType { get; }
    }

    /// <summary>
    /// Wrapper for the <see cref="ParameterInfo"/> type.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ParameterInfoWrapper : IParameterInfoWrapper
    {
        /// <summary>
        /// The wrapped <see cref="ParameterInfo"/> type.
        /// </summary>
        private readonly ParameterInfo parameterInfo;

        /// <summary>
        /// Initialises a new instance of the <see cref="ParameterInfoWrapper"/> type.
        /// </summary>
        /// <param name="parameterInfo">The <see cref="ParameterInfo"/> object to wrap.</param>
        public ParameterInfoWrapper(ParameterInfo parameterInfo)
        {
            this.parameterInfo = parameterInfo;
        }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        public string Name => this.parameterInfo.Name;

        /// <summary>
        /// Gets the type of the parameter.
        /// </summary>
        public ITypeWrapper ParameterType => new TypeWrapper(this.parameterInfo.ParameterType);
    }
}
