// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Contracts
{
    using System.Collections.Generic;
    using System.Reflection;
    using Present.CodeGeneration.Wrappers.Custom;

    /// <summary>
    /// Contract for the wrapper generator.
    /// </summary>
    public interface IWrapperGenerator
    {
        /// <summary>
        /// Generates a Roslyn namespace definition from a type and its methods.
        /// </summary>
        /// <param name="options">The program options.</param>
        /// <param name="type">The type being wrapped.</param>
        /// <param name="methods">The methods to be wrapped.</param>
        /// <returns>The generated namespace declaration.</returns>
        INamespaceDeclarationSyntaxWrapper Generate(
            IWrapOptions options,
            System.Type type,
            IEnumerable<MethodInfo> methods);
    }
}