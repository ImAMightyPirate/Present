// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Generators
{
    using EnsureThat;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Present.CodeGeneration.Contracts;

    /// <summary>
    /// Responsible for generating the Roslyn definition for an interface.
    /// </summary>
    public class InterfaceCodeGenerator : IInterfaceCodeGenerator
    {
        /// <summary>
        /// Generates a Roslyn interface definition.
        /// </summary>
        /// <param name="interfaceName">The interface name.</param>
        /// <param name="modifiers">Modifiers to be applied to the interface.</param>
        /// <returns>The generated interface declaration.</returns>
        public InterfaceDeclarationSyntax Generate(
            string interfaceName,
            SyntaxToken[] modifiers)
        {
            Ensure.That(interfaceName).IsNotNullOrWhiteSpace();
            Ensure.That(modifiers).IsNotNull();

            // Create an interface with the modifiers supplied
            return SyntaxFactory
                .InterfaceDeclaration(interfaceName)
                .AddModifiers(modifiers);
        }
    }
}
