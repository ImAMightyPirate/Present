// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Contracts
{
    using System;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Contract for the attribute code generator.
    /// </summary>
    public interface IAttributeCodeGenerator
    {
        /// <summary>
        /// Generates a Roslyn attribute definition.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="arguments">The attribute arguments.</param>
        /// <returns>The generated attribute.</returns>
        AttributeSyntax Generate(
            Type type,
            params AttributeArgumentSyntax[] arguments);

        /// <summary>
        /// Generates a Roslyn attribute definition.
        /// </summary>
        /// <param name="fullyQualifiedTypeName">The fully qualified type name.</param>
        /// <param name="arguments">The attribute arguments.</param>
        /// <returns>The generated attribute.</returns>
        AttributeSyntax Generate(
            string fullyQualifiedTypeName,
            params AttributeArgumentSyntax[] arguments);
    }
}