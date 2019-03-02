// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Generators
{
    using System.Linq;
    using EnsureThat;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Present.CodeGeneration.Contracts;

    /// <summary>
    /// Responsible for generating the Roslyn definition for a class.
    /// </summary>
    public class ClassCodeGenerator : IClassCodeGenerator
    {
        /// <summary>
        /// Generates a Roslyn class definition.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="attributes">The attributes to decorate the class.</param>
        /// <param name="modifiers">Modifiers to be applied to the class.</param>
        /// <param name="interfaceName">The name of the interface the type implements.</param>
        /// <returns>The generated class declaration.</returns>
        public ClassDeclarationSyntax Generate(
            string typeName,
            AttributeSyntax[] attributes,
            SyntaxToken[] modifiers,
            string interfaceName = null)
        {
            Ensure.That(typeName).IsNotNullOrWhiteSpace();
            Ensure.That(attributes).IsNotNull();
            Ensure.That(modifiers).IsNotNull();

            // Create a class with the modifiers specified
            var classDeclaration = SyntaxFactory
                .ClassDeclaration(typeName)
                .AddModifiers(modifiers);

            // Decorate the class with the supplied attributes (if any)
            if (attributes.Length > 0)
            {
                var attributeSyntaxList = attributes
                    .Select(attribute => SyntaxFactory.AttributeList(SyntaxFactory.SingletonSeparatedList(attribute)))
                    .ToList();

                classDeclaration = classDeclaration
                    .WithAttributeLists(new SyntaxList<AttributeListSyntax>(attributeSyntaxList));
            }

            // Have the class implement the interface (if supplied)
            if (!string.IsNullOrEmpty(interfaceName))
            {
                classDeclaration = classDeclaration
                    .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.IdentifierName(interfaceName)));
            }

            return classDeclaration;
        }
    }
}
