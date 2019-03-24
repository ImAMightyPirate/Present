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
        private const string PrivateModifier = "private";

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
            /// When the interface is generated then the interface name should be returned.
            /// </summary>
            [Test]
            public void WhenInterfaceIsGeneratedThenInterfaceNameShouldBeReturned()
            {
                // Act
                var result = this.sut.Generate(InterfaceName, new SyntaxToken[0]);

                // Assert
                result.ToString().ShouldContain(InterfaceName);
            }

            /// <summary>
            /// When the interface is generated then the modifiers should be returned.
            /// </summary>
            /// <param name="modifierSyntaxKind">The syntax kind for the modifier.</param>
            /// <param name="expectedDeclarationString">The string expected in the interface declaration.</param>
            [TestCase(SyntaxKind.PublicKeyword, PublicModifier)]
            [TestCase(SyntaxKind.PrivateKeyword, PrivateModifier)]
            public void WhenInterfaceIsGeneratedThenModifiersShouldBeReturned(
                SyntaxKind modifierSyntaxKind,
                string expectedDeclarationString)
            {
                // Act
                var result = this.sut.Generate(InterfaceName, new[] { SyntaxFactory.Token(modifierSyntaxKind) });

                // Assert
                result.ToString().ShouldContain(expectedDeclarationString);
            }
        }
    }
}
