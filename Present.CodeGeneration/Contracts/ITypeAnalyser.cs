// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Contracts
{
    using Present.CodeGeneration.Wrappers.Custom;

    /// <summary>
    /// Contract for the type analyser.
    /// </summary>
    public interface ITypeAnalyser
    {
        /// <summary>
        /// Determines whether a type can be wrapped.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True when the type can be wrapped, otherwise false.</returns>
        bool CanWrap(ITypeWrapper type);
    }
}