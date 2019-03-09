// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Wrappers.Custom
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Contract for the <see cref="MethodInfo"/> type wrapper.
    /// </summary>
    public interface IMethodInfoWrapper
    {
        /// <summary>
        /// Gets a value indicating whether the method has a special name.
        /// </summary>
        bool IsSpecialName { get; }

        /// <summary>
        /// Gets a value indicating whether the method is static.
        /// </summary>
        bool IsStatic { get; }

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the return type of the method.
        /// </summary>
        ITypeWrapper ReturnType { get; }

        /// <summary>
        /// Gets the parameters of the method.
        /// </summary>
        /// <returns>
        /// An array of wrapped <see cref="ParameterInfo"/> objects containing information that matches
        /// the signature of the method reflected by this MethodBase instance.
        /// </returns>
        IParameterInfoWrapper[] GetParameters();
    }

    /// <summary>
    /// Wrapper for the <see cref="MethodInfo"/> type.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MethodInfoWrapper : IMethodInfoWrapper
    {
        /// <summary>
        /// The wrapped <see cref="MethodInfo"/> type.
        /// </summary>
        private readonly MethodInfo methodInfo;

        /// <summary>
        /// Initialises a new instance of the <see cref="MethodInfoWrapper"/> type.
        /// </summary>
        /// <param name="methodInfo">The <see cref="MethodInfo"/> object to wrap.</param>
        public MethodInfoWrapper(MethodInfo methodInfo)
        {
            this.methodInfo = methodInfo;
        }

        /// <summary>
        /// Gets a value indicating whether the method has a special name.
        /// </summary>
        public bool IsSpecialName => this.methodInfo.IsSpecialName;

        /// <summary>
        /// Gets a value indicating whether the method is static.
        /// </summary>
        public bool IsStatic => this.methodInfo.IsStatic;

        /// <summary>
        /// Gets the name of the method.
        /// </summary>
        public string Name => this.methodInfo.Name;
        
        /// <summary>
        /// Gets the return type of the method.
        /// </summary>
        public ITypeWrapper ReturnType => new TypeWrapper(this.methodInfo.ReturnType);

        /// <summary>
        /// Gets the parameters of the method.
        /// </summary>
        /// <returns>
        /// An array of wrapped <see cref="ParameterInfo"/> objects containing information that matches
        /// the signature of the method reflected by this MethodBase instance.
        /// </returns>
        public IParameterInfoWrapper[] GetParameters()
        {
            var parameters = new List<IParameterInfoWrapper>();
            this.methodInfo.GetParameters().ToList().ForEach(p => parameters.Add(new ParameterInfoWrapper(p)));
            return parameters.ToArray();
        }
    }
}
