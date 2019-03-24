// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Tests.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.Versioning;
    using System.Text.RegularExpressions;
    using Moq;
    using NUnit.Framework;
    using Present.CodeGeneration.Generators;
    using Present.CodeGeneration.Wrappers.Custom;
    using Shouldly;

    /// <summary>
    /// Unit tests that cover the behaviour of the <see cref="XmlHeaderGenerator"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class XmlHeaderGeneratorTests
    {
        private const string AssemblyQualifiedName = "Assembly";
        private const string FrameworkName = ".NET";

        private XmlHeaderGenerator sut;

        private Mock<IAssemblyWrapper> assemblyWrapperMock;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.assemblyWrapperMock = new Mock<IAssemblyWrapper>();

            this.sut = new XmlHeaderGenerator(
                this.assemblyWrapperMock.Object);
        }

        /// <summary>
        /// Unit tests that cover the behaviour of the <see cref="XmlHeaderGenerator.Generate"/> method.
        /// </summary>
        [TestFixture]
        public class Generate : XmlHeaderGeneratorTests
        {
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
                    assemblyQualifiedName));
            }

            /// <summary>
            /// When the entry assembly has target framework attribute of a defined count then a generated syntax
            /// trivia list contianing the framework names should be returned.
            /// </summary>
            /// <param name="targetFrameworkAttributeCount">The number of target framework attributes for the entry assembly.</param>
            [TestCase(1)]
            [TestCase(2)]
            public void WhenEntryAssemblyHasTargetFrameworkAttributeOfCountThenGeneratedSyntaxTriviaListContainingFrameworkNamesShouldBeReturned(
                int targetFrameworkAttributeCount)
            {
                // Arrange
                var attributes = new List<TargetFrameworkAttribute>(targetFrameworkAttributeCount);

                for (var i = 0; i < targetFrameworkAttributeCount; i++)
                {
                    attributes.Add(new TargetFrameworkAttribute(FrameworkName));
                }

                this.assemblyWrapperMock
                    .Setup(a => a.GetEntryAssemblyTargetFrameworkAttributes())
                    .Returns(attributes);

                // Act
                var result = this.sut.Generate(AssemblyQualifiedName);

                // Assert
                result
                    .Select(r => r.ToString())
                    .Any(r => Regex.Matches(r, FrameworkName).Count == targetFrameworkAttributeCount)
                    .ShouldBeTrue();
            }
        }
    }
}
