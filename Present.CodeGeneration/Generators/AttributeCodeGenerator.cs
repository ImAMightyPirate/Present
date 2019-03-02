// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Generators
{
    using System;
    using System.Linq;
    using EnsureThat;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using Present.CodeGeneration.Contracts;

    /// <summary>
    /// Responsible for generating the Roslyn definition for an attribute.
    /// </summary>
    public class AttributeCodeGenerator : IAttributeCodeGenerator
    {
        /// <summary>
        /// Generates a Roslyn attribute definition.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="arguments">The attribute arguments.</param>
        /// <returns>The generated attribute.</returns>
        public AttributeSyntax Generate(
            Type type,
            params AttributeArgumentSyntax[] arguments)
        {
            Ensure.That(type).IsNotNull();

            var attribute = SyntaxFactory.Attribute(
                SyntaxFactory.QualifiedName(
                    SyntaxFactory.IdentifierName(type.Namespace),
                    SyntaxFactory.IdentifierName(type.Name)));

            return this.PopulateArguments(attribute, arguments);
        }

        /// <summary>
        /// Generates a Roslyn attribute definition.
        /// </summary>
        /// <param name="fullyQualifiedTypeName">The fully qualified type name.</param>
        /// <param name="arguments">The attribute arguments.</param>
        /// <returns>The generated attribute.</returns>
        public AttributeSyntax Generate(
            string fullyQualifiedTypeName,
            params AttributeArgumentSyntax[] arguments)
        {
            Ensure.That(fullyQualifiedTypeName).IsNotEmptyOrWhitespace();

            var attribute = SyntaxFactory.Attribute(
                SyntaxFactory.IdentifierName(fullyQualifiedTypeName));

            return this.PopulateArguments(attribute, arguments);
        }

        /// <summary>
        /// Adds the arguments to the attribute.
        /// </summary>
        /// <param name="attribute">The attribute syntax.</param>
        /// <param name="arguments">The arguments to be added.</param>
        /// <returns>The attribute with arguments attached.</returns>
        private AttributeSyntax PopulateArguments(
            AttributeSyntax attribute,
            AttributeArgumentSyntax[] arguments)
        {
            if (arguments.Length == 0)
            {
                return attribute;
            }

            return attribute.WithArgumentList(
                SyntaxFactory.AttributeArgumentList(
                    SyntaxFactory.SeparatedList(arguments.Select(argument => argument))));
        }
    }
}