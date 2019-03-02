// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Contracts
{
    /// <summary>
    /// Contract for the type wrapper.
    /// </summary>
    public interface ITypeWrapper
    {
        /// <summary>
        /// Creates a wrapper class for a .NET type and outputs
        /// the code file.
        /// </summary>
        /// <param name="assemblyQualifiedTypeName">The assembly qualified name of the .NET type to be wrapped.</param>
        /// <param name="outputPath">The output path of the generated code file.</param>
        void Wrap(string assemblyQualifiedTypeName, string outputPath);
    }
}