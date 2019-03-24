// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Tests.Generators
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using NUnit.Framework;
    using Present.CodeGeneration.Generators;
    using Shouldly;

    /// <summary>
    /// Unit tests that cover the behaviour of the <see cref="ClassCodeGenerator"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class ClassCodeGeneratorTests
    {
        private const string AttributeName = "AttributeA";
        private const string TypeName = "TypeB";
        private const string InterfaceName = "InterfaceC";
        private const string PublicModifier = "public";

        private ClassCodeGenerator sut;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.sut = new ClassCodeGenerator();
        }

        /// <summary>
        /// Unit tests that cover the behaviour of the <see cref="ClassCodeGenerator.Generate"/> method.
        /// </summary>
        [TestFixture]
        public class Generate : ClassCodeGeneratorTests
        {
            /// <summary>
            /// When the type name argument is invalid then a <see cref="ArgumentException"/> should be thrown.
            /// </summary>
            /// <param name="typeName">The type name.</param>
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            public void WhenTypeNameArgumentIsInvalidThenArgumentExceptionShouldBeThrown(string typeName)
            {
                // Act + Assert
                Should.Throw<ArgumentException>(() => this.sut.Generate(
                    typeName,
                    new AttributeSyntax[0],
                    new SyntaxToken[0]));
            }

            /// <summary>
            /// When the attributes argument is null then a <see cref="ArgumentNullException"/> should be thrown.
            /// </summary>
            [Test]
            public void WhenAttributesArgumentIsNullThenArgumentNullExceptionShouldBeThrown()
            {
                // Act + Assert
                Should.Throw<ArgumentNullException>(() => this.sut.Generate(
                    TypeName,
                    null,
                    new SyntaxToken[0]));
            }

            /// <summary>
            /// When the modifiers argument is null then a <see cref="ArgumentNullException"/> should be thrown.
            /// </summary>
            [Test]
            public void WhenModifiersArgumentIsNullThenArgumentNullExceptionShouldBeThrown()
            {
                // Act + Assert
                Should.Throw<ArgumentNullException>(() => this.sut.Generate(
                    TypeName,
                    new AttributeSyntax[0],
                    null));
            }

            /// <summary>
            /// When an attribute is supplied then the class declaration should include the attribute.
            /// </summary>
            [Test]
            public void WhenAttributeIsSuppliedThenClassDeclarationShouldIncludeAttribute()
            {
                // Act
                var result = this.sut.Generate(
                    TypeName,
                    new[] { SyntaxFactory.Attribute(SyntaxFactory.IdentifierName(AttributeName)) },
                    new SyntaxToken[0]);

                // Assert
                result.ToString().ShouldContain($"[{AttributeName}]");
            }

            /// <summary>
            /// When a modifier is supplied then the class declaration should include the modifier.
            /// </summary>
            [Test]
            public void WhenModifierIsSuppliedThenClassDeclarationShouldIncludeModifier()
            {
                // Act
                var result = this.sut.Generate(
                    TypeName,
                    new AttributeSyntax[0],
                    new[] { SyntaxFactory.Token(SyntaxKind.PublicKeyword) });

                // Assert
                result.ToString().ShouldContain(PublicModifier);
            }

            /// <summary>
            /// When an interface name is supplied then the class declaration should include the interface.
            /// </summary>
            [Test]
            public void WhenInterfaceNameIsSuppliedThenClassDeclarationShouldIncludeInterface()
            {
                // Act
                var result = this.sut.Generate(
                    TypeName,
                    new AttributeSyntax[0],
                    new SyntaxToken[0],
                    InterfaceName);

                // Assert
                result.ToString().ShouldContain(InterfaceName);
            }
        }
    }
}
