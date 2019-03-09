// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using Ninject.Extensions.Logging;
    using NUnit.Framework;
    using Present.CodeGeneration.Contracts;
    using Present.CodeGeneration.Wrappers.Custom;
    using Shouldly;

    /// <summary>
    /// Unit tests that cover the behaviour of the <see cref="MethodAnalyser"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class MethodAnalyserTests
    {
        private MethodAnalyser sut;

        private Mock<ILogger> loggerMock;
        private Mock<ITypeAnalyser> typeAnalyserMock;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.loggerMock = new Mock<ILogger>();
            this.typeAnalyserMock = new Mock<ITypeAnalyser>();

            this.sut = new MethodAnalyser(
                this.loggerMock.Object,
                this.typeAnalyserMock.Object);
        }

        /// <summary>
        /// Unit tests that cover the behaviour of the <see cref="MethodAnalyser.CanWrap"/> method.
        /// </summary>
        [TestFixture]
        public class CanWrap : MethodAnalyserTests
        {
            /// <summary>
            /// When the method argument is null then a <see cref="ArgumentNullException"/> should be thrown.
            /// </summary>
            [Test]
            public void WhenMethodArgumentIsNullThenArgumentNullExceptionShouldBeThrown()
            {
                // Act + Assert
                Should.Throw<ArgumentNullException>(() => this.sut.CanWrap(
                    null));
            }

            /// <summary>
            /// When method is not static then false should be returned.
            /// </summary>
            [Test]
            public void WhenMethodIsNotStaticThenFalseShouldBeReturned()
            {
                // Arrange
                var methodInfoWrapperMock = new Mock<IMethodInfoWrapper>();
                methodInfoWrapperMock.Setup(m => m.IsStatic).Returns(false);

                // Act
                var result = this.sut.CanWrap(methodInfoWrapperMock.Object);

                // Assert
                result.ShouldBeFalse();
            }

            /// <summary>
            /// When method has a special name then false should be returned.
            /// </summary>
            [Test]
            public void WhenMethodHasSpecialNameThenFalseShouldBeReturned()
            {
                // Arrange
                var methodInfoWrapperMock = new Mock<IMethodInfoWrapper>();
                methodInfoWrapperMock.Setup(m => m.IsStatic).Returns(true);
                methodInfoWrapperMock.Setup(m => m.IsSpecialName).Returns(true);

                // Act
                var result = this.sut.CanWrap(methodInfoWrapperMock.Object);

                // Assert
                result.ShouldBeFalse();
            }

            /// <summary>
            /// When method has a return type that cannot be wrapped then false should be returned.
            /// </summary>
            [Test]
            public void WhenMethodHasReturnTypeThatCannotBeWrappedThenFalseShouldBeReturned()
            {
                // Arrange
                var methodInfoWrapperMock = new Mock<IMethodInfoWrapper>();
                methodInfoWrapperMock.Setup(m => m.IsStatic).Returns(true);

                // The return type cannot be wrapped
                this.typeAnalyserMock.Setup(t => t.CanWrap(methodInfoWrapperMock.Object.ReturnType)).Returns(false);

                // Act
                var result = this.sut.CanWrap(methodInfoWrapperMock.Object);

                // Assert
                result.ShouldBeFalse();
            }

            /// <summary>
            /// When method has a parameter type that cannot be wrapped then false should be returned.
            /// </summary>
            [Test]
            public void WhenMethodHasParameterTypeThatCannotBeWrappedThenFalseShouldBeReturned()
            {
                // Arrange
                var parameterInfoWrapperMock = new Mock<IParameterInfoWrapper>();

                var methodInfoWrapperMock = new Mock<IMethodInfoWrapper>();
                methodInfoWrapperMock.Setup(m => m.GetParameters()).Returns(new[] { parameterInfoWrapperMock.Object });
                methodInfoWrapperMock.Setup(m => m.IsStatic).Returns(true);

                // Only the return type can be wrapped
                this.typeAnalyserMock.Setup(t => t.CanWrap(methodInfoWrapperMock.Object.ReturnType)).Returns(true);
                this.typeAnalyserMock.Setup(t => t.CanWrap(parameterInfoWrapperMock.Object.ParameterType)).Returns(false);

                // Act
                var result = this.sut.CanWrap(methodInfoWrapperMock.Object);

                // Assert
                result.ShouldBeFalse();
            }

            /// <summary>
            /// When method is static and has return and parameter types that can be returned then true should be returned.
            /// </summary>
            [Test]
            public void WhenMethodIsStaticAndHasReturnAndParameterTypesThatCanBeWrappedThenTrueShouldBeReturned()
            {
                // Arrange
                var parameterInfoWrapperMock = new Mock<IParameterInfoWrapper>();

                var methodInfoWrapperMock = new Mock<IMethodInfoWrapper>();
                methodInfoWrapperMock.Setup(m => m.GetParameters()).Returns(new[] { parameterInfoWrapperMock.Object });
                methodInfoWrapperMock.Setup(m => m.IsStatic).Returns(true);

                // Both the return type and parameter type can be wrapped
                this.typeAnalyserMock.Setup(t => t.CanWrap(methodInfoWrapperMock.Object.ReturnType)).Returns(true);
                this.typeAnalyserMock.Setup(t => t.CanWrap(parameterInfoWrapperMock.Object.ParameterType)).Returns(true);

                // Act
                var result = this.sut.CanWrap(methodInfoWrapperMock.Object);

                // Assert
                result.ShouldBeTrue();
            }
        }
    }
}