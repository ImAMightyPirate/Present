// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Contracts
{
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Contract for the interface code generator.
    /// </summary>
    public interface IInterfaceCodeGenerator
    {
        /// <summary>
        /// Generates a Roslyn interface definition.
        /// </summary>
        /// <param name="interfaceName">The interface name.</param>
        /// <returns>The generated interface declaration.</returns>
        InterfaceDeclarationSyntax Generate(string interfaceName);
    }
}