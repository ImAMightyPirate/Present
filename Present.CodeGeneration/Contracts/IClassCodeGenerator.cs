﻿// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Contracts
{
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Contract for the class code generator.
    /// </summary>
    public interface IClassCodeGenerator
    {
        /// <summary>
        /// Generates a Roslyn class definition.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="attributes">The attributes to decorate the class.</param>
        /// <param name="modifiers">Modifiers to be applied to the class.</param>
        /// <param name="interfaceName">The name of the interface the type implements.</param>
        /// <returns>The generated class declaration.</returns>
        ClassDeclarationSyntax Generate(
            string typeName,
            AttributeSyntax[] attributes,
            SyntaxToken[] modifiers,
            string interfaceName = null);
    }
}