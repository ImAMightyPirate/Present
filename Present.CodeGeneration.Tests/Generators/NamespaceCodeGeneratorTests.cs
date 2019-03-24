// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Tests.Generators
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using NUnit.Framework;
    using Present.CodeGeneration.Constants;
    using Present.CodeGeneration.Contracts;
    using Present.CodeGeneration.Generators;
    using Shouldly;

    /// <summary>
    /// Unit tests that cover the behaviour of the <see cref="NamespaceCodeGenerator"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class NamespaceCodeGeneratorTests
    {
        private const string AssemblyQualifiedName = "Assembly";
        private const string SystemNamespace = "System.Namespace";
        private const string OtherNamespace = "Other.Namespace";

        private NamespaceCodeGenerator sut;

        private Mock<IXmlHeaderGenerator> xmlHeaderGeneratorMock;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.xmlHeaderGeneratorMock = new Mock<IXmlHeaderGenerator>();

            this.sut = new NamespaceCodeGenerator(
                this.xmlHeaderGeneratorMock.Object);
        }

        /// <summary>
        /// Unit tests that cover the behaviour of the <see cref="NamespaceCodeGenerator.Generate"/> method.
        /// </summary>
        [TestFixture]
        public class Generate : NamespaceCodeGeneratorTests
        {
            /// <summary>
            /// When the type namespace argument is invalid then a <see cref="ArgumentException"/> should be thrown.
            /// </summary>
            /// <param name="typeNamespace">The type namespace argument value.</param>
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            public void WhenTypeNamespaceArgumentIsInvalidThenArgumentExceptionShouldBeThrown(string typeNamespace)
            {
                // Act + Assert
                Should.Throw<ArgumentException>(() => this.sut.Generate(
                    typeNamespace,
                    AssemblyQualifiedName));
            }

            /// <summary>
            /// When the assembly qualified name argument is invalid then a <see cref="ArgumentException"/> should be thrown.
            /// </summary>
            /// <param name="assemblyQualifiedName">The assembly qualified name argument value.</param>
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            public void WhenAssemblyQualifiedNameArgumentIsInvalidThenArgumentExceptionShouldBeThrown(string assemblyQualifiedName)
            {
                // Act + Assert
                Should.Throw<ArgumentException>(() => this.sut.Generate(
                    SystemNamespace,
                    assemblyQualifiedName));
            }

            /// <summary>
            /// When the namespace starts with System then it should be substituted with Present.
            /// </summary>
            [Test]
            public void WhenNamespaceStartsWithSystemThenSystemShouldBeSubstitutedWithPresent()
            {
                // Act
                var result = this.sut.Generate(SystemNamespace, AssemblyQualifiedName);

                // Assert
                result.ToString().Contains(Namespace.Present).ShouldBeTrue();
            }

            /// <summary>
            /// When the namespace does not start with System then it should not be substituted with Present.
            /// </summary>
            [Test]
            public void WhenNamespaceDoesNotStartWithSystemThenSystemShouldNotBeSubstitutedWithPresent()
            {
                // Act
                var result = this.sut.Generate(OtherNamespace, AssemblyQualifiedName);

                // Assert
                result.ToString().Contains(Namespace.Present).ShouldBeFalse();
            }
        }
    }
}
