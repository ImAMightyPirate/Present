// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using NUnit.Framework;
    using Present.CodeGeneration.Wrappers.Custom;
    using Shouldly;

    /// <summary>
    /// Unit tests that cover the behaviour of the <see cref="TypeAnalyser"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class TypeAnalyserTests
    {
        private TypeAnalyser sut;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.sut = new TypeAnalyser();
        }

        /// <summary>
        /// Unit tests that cover the behaviour of the <see cref="TypeAnalyser.CanWrap"/> method.
        /// </summary>
        [TestFixture]
        public class CanWrap : TypeAnalyserTests
        {
            /// <summary>
            /// When the type argument is null then a <see cref="ArgumentNullException"/> should be thrown.
            /// </summary>
            [Test]
            public void WhenTypeArgumentIsNullThenArgumentNullExceptionShouldBeThrown()
            {
                // Act + Assert
                Should.Throw<ArgumentNullException>(() => this.sut.CanWrap(
                    null));
            }

            /// <summary>
            /// When type is by-ref like then false should be returned.
            /// </summary>
            [Test]
            public void WhenTypeIsByRefLikeThenFalseShouldBeReturned()
            {
                // Arrange
                var typeWrapperMock = new Mock<ITypeWrapper>();
                typeWrapperMock.Setup(t => t.IsByRefLike).Returns(true);

                // Act
                var result = this.sut.CanWrap(typeWrapperMock.Object);

                // Assert
                result.ShouldBeFalse();
            }

            /// <summary>
            /// When type is a value type then true should be returned.
            /// </summary>
            [Test]
            public void WhenTypeIsValueTypeThenTrueShouldBeReturned()
            {
                // Arrange
                var typeWrapperMock = new Mock<ITypeWrapper>();
                typeWrapperMock.Setup(t => t.IsValueType).Returns(true);

                // Act
                var result = this.sut.CanWrap(typeWrapperMock.Object);

                // Assert
                result.ShouldBeTrue();
            }

            /// <summary>
            /// When type is a supported reference type then true should be returned.
            /// </summary>
            [Test]
            public void WhenTypeIsSupportedReferenceTypeThenTrueShouldBeReturned()
            {
                // Arrange
                var typeWrapperMock = new Mock<ITypeWrapper>();
                typeWrapperMock.Setup(t => t.IsOf(It.IsAny<Type>())).Returns(true);

                // Act
                var result = this.sut.CanWrap(typeWrapperMock.Object);

                // Assert
                result.ShouldBeTrue();
            }

            /// <summary>
            /// When type is an array of value types then true should be returned.
            /// </summary>
            [Test]
            public void WhenTypeIsArrayOfValueTypesThenTrueShouldBeReturned()
            {
                // Arrange
                var elementTypeWrapperMock = new Mock<ITypeWrapper>();
                elementTypeWrapperMock.Setup(t => t.IsValueType).Returns(true);

                var typeWrapperMock = new Mock<ITypeWrapper>();
                typeWrapperMock.Setup(t => t.IsArray).Returns(true);
                typeWrapperMock.Setup(t => t.GetElementType()).Returns(elementTypeWrapperMock.Object);

                // Act
                var result = this.sut.CanWrap(typeWrapperMock.Object);

                // Assert
                result.ShouldBeTrue();
            }

            /// <summary>
            /// When type is an array of supported reference types then true should be returned.
            /// </summary>
            [Test]
            public void WhenTypeIsArrayOfSupportedReferenceTypesThenTrueShouldBeReturned()
            {
                // Arrange
                var elementTypeWrapperMock = new Mock<ITypeWrapper>();
                elementTypeWrapperMock.Setup(t => t.IsOf(It.IsAny<Type>())).Returns(true);

                var typeWrapperMock = new Mock<ITypeWrapper>();
                typeWrapperMock.Setup(t => t.IsArray).Returns(true);
                typeWrapperMock.Setup(t => t.GetElementType()).Returns(elementTypeWrapperMock.Object);

                // Act
                var result = this.sut.CanWrap(typeWrapperMock.Object);

                // Assert
                result.ShouldBeTrue();
            }
        }
    }
}