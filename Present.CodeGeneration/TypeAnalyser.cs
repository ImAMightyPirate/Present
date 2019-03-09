// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration
{
    using System;
    using System.Text;
    using EnsureThat;
    using Present.CodeGeneration.Contracts;
    using Present.CodeGeneration.Wrappers.Custom;

    /// <summary>
    /// Class responsible for determining whether a type can have a wrapper automatically
    /// generated.
    /// </summary>
    public class TypeAnalyser : ITypeAnalyser
    {
        /// <summary>
        /// Determines whether a type can be wrapped.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>True when the type can be wrapped, otherwise false.</returns>
        public bool CanWrap(ITypeWrapper type)
        {
            Ensure.That(type).IsNotNull();

            // If an array then check the type of the element
            if (type.IsArray)
            {
                return this.CanWrap(type.GetElementType());
            }

            // Common reference types that cause no difficulties with mocking are supported
            if (type.IsOf(typeof(string))
                || type.IsOf(typeof(Type))
                || type.IsOf(typeof(Encoding)))
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