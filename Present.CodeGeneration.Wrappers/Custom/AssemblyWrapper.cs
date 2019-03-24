// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Wrappers.Custom
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Versioning;

    /// <summary>
    /// Contract for the <see cref="Assembly"/> type wrapper.
    /// </summary>
    public interface IAssemblyWrapper
    {
        /// <summary>
        /// Gets the target framework attributes for the entry assembly.
        /// </summary>
        /// <returns>An enumerable that contains the target framework attributes for the entry assembly.</returns>
        IEnumerable<TargetFrameworkAttribute> GetEntryAssemblyTargetFrameworkAttributes();
    }

    /// <summary>
    /// Wrapper for the <see cref="Assembly"/> type.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AssemblyWrapper : IAssemblyWrapper
    {
        /// <summary>
        /// Gets the target framework attributes for the entry assembly.
        /// </summary>
        /// <returns>An enumerable that contains the target framework attributes for the entry assembly.</returns>
        public IEnumerable<TargetFrameworkAttribute> GetEntryAssemblyTargetFrameworkAttributes()
        {
            return Assembly
                .GetEntryAssembly()
                .GetCustomAttributes()
                .OfType<TargetFrameworkAttribute>();
        }
    }
}
