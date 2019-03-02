// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Generators
{
    using EnsureThat;
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
        /// <returns>The generated interface declaration.</returns>
        public InterfaceDeclarationSyntax Generate(string interfaceName)
        {
            Ensure.That(interfaceName).IsNotNullOrWhiteSpace();

            // Create a public interface
            return SyntaxFactory
                .InterfaceDeclaration(interfaceName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PartialKeyword));
        }
    }
}
