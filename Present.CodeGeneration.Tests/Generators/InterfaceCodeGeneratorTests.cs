// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Tests.Generators
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using NUnit.Framework;
    using Present.CodeGeneration.Generators;
    using Shouldly;

    /// <summary>
    /// Unit tests that cover the behaviour of the <see cref="InterfaceCodeGenerator"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class InterfaceCodeGeneratorTests
    {
        private const string InterfaceName = "Interface";
        private const string PublicModifier = "public";

        private InterfaceCodeGenerator sut;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.sut = new InterfaceCodeGenerator();
        }

        /// <summary>
        /// Unit tests that cover the behaviour of the <see cref="InterfaceCodeGenerator.Generate"/> method.
        /// </summary>
        [TestFixture]
        public class Generate : InterfaceCodeGeneratorTests
        {
            /// <summary>
            /// When the interface name argument is invalid then a <see cref="ArgumentException"/> should be thrown.
            /// </summary>
            /// <param name="interfaceName">The interface name.</param>
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            public void WhenInterfaceNameArgumentIsInvalidThenArgumentExceptionShouldBeThrown(string interfaceName)
            {
                // Act + Assert
                Should.Throw<ArgumentException>(() => this.sut.Generate(
                    interfaceName,
                    new SyntaxToken[0]));
            }

            /// <summary>
            /// When the modifiers argument is invalid then a <see cref="ArgumentNullException"/> should be thrown.
            /// </summary>
            [Test]
            public void WhenModifiersArgumentIsInvalidThenArgumentExceptionShouldBeThrown()
            {
                // Act + Assert
                Should.Throw<ArgumentNullException>(() => this.sut.Generate(
                    InterfaceName,
                    null));
            }

            /// <summary>
            /// When the interface is generated then the interface name should be used.
            /// </summary>
            [Test]
            public void WhenInterfaceIsGeneratedThenInterfaceNameShouldBeUsed()
            {
                // Act
                var result = this.sut.Generate(InterfaceName, new SyntaxToken[0]);

                // Assert
                result.ToString().ShouldContain(InterfaceName);
            }

            /// <summary>
            /// When a modifier is supplied then the interface declaration should include the modifier.
            /// </summary>
            [Test]
            public void WhenModifierIsSuppliedThenInterfaceDeclarationShouldIncludeModifier()
            {
                // Act
                var result = this.sut.Generate(InterfaceName, new[] { SyntaxFactory.Token(SyntaxKind.PublicKeyword) });

                // Assert
                result.ToString().ShouldContain(PublicModifier);
            }
        }
    }
}
