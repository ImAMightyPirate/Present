// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Tests.Dummies
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A type that exposes the count of methods it has available.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class TypeWithKnownMethodCountDummy
    {
        /// <summary>
        /// Gets the number of methods contained in this type.
        /// </summary>
        public static int MethodCount => typeof(TypeWithKnownMethodCountDummy).GetMethods().Length;
    }
}
