// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Wrappers.Custom
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Contract for the <see cref="System.Type"/> type wrapper.
    /// </summary>
    public interface ITypeWrapper
    {
        /// <summary>
        /// Gets a value indicating whether the type is an array.
        /// </summary>
        bool IsArray { get; }

        /// <summary>
        /// Gets a value indicating whether the type is by-ref like.
        /// </summary>
        /// <remark>
        /// Reference structures such as <see cref="Span{T}"/>.
        /// </remark>
        bool IsByRefLike { get; }

        /// <summary>
        /// Gets a value indicating whether the type is a value type.
        /// </summary>
        bool IsValueType { get; }

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the element type of an array.
        /// </summary>
        ITypeWrapper GetElementType();

        /// <summary>
        /// Determines whether the type held in the wrapper is of the same type.
        /// </summary>
        /// <param name="otherType">The type to compare.</param>
        /// <returns>A value indicating whether the types are equal.</returns>
        bool IsOf(Type otherType);
    }

    /// <summary>
    /// Wrapper for the <see cref="System.Type"/> type.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TypeWrapper : ITypeWrapper
    {
        /// <summary>
        /// The wrapped <see cref="System.Type"/> type.
        /// </summary>
        private readonly System.Type type;

        /// <summary>
        /// Initialises a new instance of the <see cref="TypeWrapper"/> type.
        /// </summary>
        /// <param name="type">The <see cref="System.Type"/> object to wrap.</param>
        public TypeWrapper(System.Type type)
        {
            this.type = type;
        }

        /// <summary>
        /// Gets a value indicating whether the type is an array.
        /// </summary>
        public bool IsArray => this.type.IsArray;

        /// <summary>
        /// Gets a value indicating whether the type is by-ref like.
        /// </summary>
        /// <remark>
        /// Reference structures such as <see cref="Span{T}"/>.
        /// </remark>
        public bool IsByRefLike => this.type.IsByRefLike;

        /// <summary>
        /// Gets a value indicating whether the type is a value type.
        /// </summary>
        public bool IsValueType => this.type.IsValueType;

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public string Name => this.type.Name;

        /// <summary>
        /// Gets the element type of an array.
        /// </summary>
        public ITypeWrapper GetElementType()
        {
            return new TypeWrapper(this.type.GetElementType());
        }

        /// <summary>
        /// Determines whether the type held in the wrapper is of the same type.
        /// </summary>
        /// <param name="otherType">The type to compare.</param>
        /// <returns>A value indicating whether the types are equal.</returns>
        public bool IsOf(Type otherType)
        {
            return this.type == otherType;
        }
    }
}
