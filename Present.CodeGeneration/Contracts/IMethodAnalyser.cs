// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Contracts
{
    using System.Reflection;

    /// <summary>
    /// Contract for the method analyser.
    /// </summary>
    public interface IMethodAnalyser
    {
        /// <summary>
        /// Determines whether a method can be wrapped automatically
        /// by the Present code generator.
        /// </summary>
        /// <param name="method">The method to analyse.</param>
        /// <returns>Returns true when the method can be wrapped automatially, otherwise false.</returns>
        bool IsWrappingSupported(MethodInfo method);
    }
}