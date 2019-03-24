// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Moq;
    using NUnit.Framework;
    using Present.CodeGeneration.Contracts;
    using Present.CodeGeneration.Tests.Dummies;
    using Present.CodeGeneration.Wrappers.Custom;
    using Shouldly;
    using ILogger = Ninject.Extensions.Logging.ILogger;
    using WrappedTypeBuilder = Present.CodeGeneration.WrappedTypeBuilder;

    /// <summary>
    /// Unit tests that cover the behaviour of the <see cref="WrappedTypeBuilder"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class WrappedTypeBuilderTests
    {
        private const string OutputPath = @"C:\Some\Path";

        private WrappedTypeBuilder sut;

        private Mock<ILogger> loggerMock;
        private Mock<IType> typeMock;
        private Mock<IMethodAnalyser> methodAnalyserMock;
        private Mock<IWrapperGenerator> wrapperGeneratorMock;
        private Mock<ICodeFileWriter> codeFileWriterMock;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.loggerMock = new Mock<ILogger>();
            this.typeMock = new Mock<IType>();
            this.methodAnalyserMock = new Mock<IMethodAnalyser>();
            this.wrapperGeneratorMock = new Mock<IWrapperGenerator>();
            this.codeFileWriterMock = new Mock<ICodeFileWriter>();

            this.sut = new WrappedTypeBuilder(
                this.loggerMock.Object,
                this.typeMock.Object,
                this.methodAnalyserMock.Object,
                this.wrapperGeneratorMock.Object,
                this.codeFileWriterMock.Object);
        }

        /// <summary>
        /// Unit tests that cover the behaviour of the <see cref="WrappedTypeBuilder.Wrap"/> method.
        /// </summary>
        [TestFixture]
        public class Wrap : WrappedTypeBuilderTests
        {
            /// <summary>
            /// When the options argument is null then a <see cref="ArgumentNullException"/> should be thrown.
            /// </summary>
            [Test]
            public void WhenOptionsArgumentIsNullThenArgumentNullExceptionShouldBeThrown()
            {
                // Act + Assert
                Should.Throw<ArgumentNullException>(() => this.sut.Wrap(null));
            }

            /// <summary>
            /// When the assembly qualified type names property is null then a <see cref="ArgumentNullException"/> should be thrown.
            /// </summary>
            [Test]
            public void WhenAssemblyQualifiedTypeNamesPropertyIsNullThenArgumentNullExceptionShouldBeThrown()
            {
                // Arrange
                var options = new Mock<IWrapOptions>();
                options.Setup(o => o.AssemblyQualifiedTypeNames).Returns<string[]>(null);

                // Act + Assert
                Should.Throw<ArgumentNullException>(() => this.sut.Wrap(options.Object));
            }

            /// <summary>
            /// When the output path property is null then a <see cref="ArgumentNullException"/> should be thrown.
            /// </summary>
            [Test]
            public void WhenOutputPathPropertyIsNullThenArgumentNullExceptionShouldBeThrown()
            {
                // Arrange
                var options = new Mock<IWrapOptions>();
                options.Setup(o => o.OutputPath).Returns<string>(null);

                // Act + Assert
                Should.Throw<ArgumentNullException>(() => this.sut.Wrap(options.Object));
            }

            /// <summary>
            /// When the output path property is invalid then a <see cref="ArgumentException"/> should be thrown.
            /// </summary>
            /// <param name="outputPath">The output path property value.</param>
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            public void WhenOutputPathPropertyIsInvalidThenArgumentExceptionShouldBeThrown(string outputPath)
            {
                // Arrange
                var options = new Mock<IWrapOptions>();
                options.Setup(o => o.OutputPath).Returns(outputPath);

                // Act + Assert
                Should.Throw<ArgumentException>(() => this.sut.Wrap(options.Object));
            }

            /// <summary>
            /// When the assembly qualified type name cannot be resolved then a code file should not be created.
            /// </summary>
            [Test]
            public void WhenAssemblyQualifiedTypeNameCannotBeResolvedThenCodeFileShouldNotBeCreated()
            {
                // Arrange
                var options = new Mock<IWrapOptions>();
                options.Setup(o => o.AssemblyQualifiedTypeNames).Returns(new string[1]);
                options.Setup(o => o.OutputPath).Returns(OutputPath);

                // Provide null for the type
                this.typeMock.Setup(t => t.GetType(It.IsAny<string>())).Returns((Type)null);

                // Act
                this.sut.Wrap(options.Object);

                // Assert
                this.codeFileWriterMock
                    .Verify(
                        w => w.WriteCodeFileToPath(It.IsAny<string>(), It.IsAny<INamespaceDeclarationSyntaxWrapper>(), It.IsAny<string>()),
                        Times.Never);
            }

            /// <summary>
            /// When valid then one code file per type should be created.
            /// </summary>
            /// <param name="typeNamesCount">The number of type names.</param>
            [TestCase(0)]
            [TestCase(1)]
            [TestCase(2)]
            public void WhenValidThenOneCodeFilePerTypeShouldBeCreated(int typeNamesCount)
            {
                // Arrange
                var options = new Mock<IWrapOptions>();
                options.Setup(o => o.AssemblyQualifiedTypeNames).Returns(new string[typeNamesCount]);
                options.Setup(o => o.OutputPath).Returns(OutputPath);

                // Provide a type with a known number of methods
                this.typeMock.Setup(t => t.GetType(It.IsAny<string>())).Returns(typeof(TypeWithKnownMethodCountDummy));

                // All methods can be wrapped
                this.methodAnalyserMock.Setup(a => a.CanWrap(It.IsAny<IMethodInfoWrapper>())).Returns(true);

                // Act
                this.sut.Wrap(options.Object);

                // Assert
                this.codeFileWriterMock
                    .Verify(
                        w => w.WriteCodeFileToPath(It.IsAny<string>(), It.IsAny<INamespaceDeclarationSyntaxWrapper>(), It.IsAny<string>()),
                        Times.Exactly(typeNamesCount));
            }

            /// <summary>
            /// When method can be wrapped then method should be included in code generation.
            /// </summary>
            [Test]
            public void WhenMethodCanBeWrappedThenMethodShouldBeIncludedInCodeGeneration()
            {
                // Arrange
                var options = new Mock<IWrapOptions>();
                options.Setup(o => o.AssemblyQualifiedTypeNames).Returns(new string[1]);
                options.Setup(o => o.OutputPath).Returns(OutputPath);

                // Provide a type with a known number of methods
                this.typeMock.Setup(t => t.GetType(It.IsAny<string>())).Returns(typeof(TypeWithKnownMethodCountDummy));

                // All methods can be wrapped
                this.methodAnalyserMock.Setup(a => a.CanWrap(It.IsAny<IMethodInfoWrapper>())).Returns(true);

                var supportedMethodsCount = 0;

                this.wrapperGeneratorMock
                    .Setup(g => g.Generate(It.IsAny<IWrapOptions>(), It.IsAny<Type>(), It.IsAny<IEnumerable<MethodInfo>>()))
                    .Callback<IWrapOptions, Type, IEnumerable<MethodInfo>>((verifyOptions, verifyType, verifyMethods) =>
                    {
                        supportedMethodsCount = verifyMethods.Count();
                    });

                // Act
                this.sut.Wrap(options.Object);

                // Assert
                supportedMethodsCount.ShouldBe(TypeWithKnownMethodCountDummy.MethodCount);
            }

            /// <summary>
            /// When method cannot be wrapped then method should be excluded from code generation.
            /// </summary>
            [Test]
            public void WhenMethodCannotBeWrappedThenMethodShouldBeExcludedFromCodeGeneration()
            {
                // Arrange
                var options = new Mock<IWrapOptions>();
                options.Setup(o => o.AssemblyQualifiedTypeNames).Returns(new string[1]);
                options.Setup(o => o.OutputPath).Returns(OutputPath);

                // Provide a type with a known number of methods
                this.typeMock.Setup(t => t.GetType(It.IsAny<string>())).Returns(typeof(TypeWithKnownMethodCountDummy));

                // All methods cannot be wrapped
                this.methodAnalyserMock.Setup(a => a.CanWrap(It.IsAny<IMethodInfoWrapper>())).Returns(false);

                var supportedMethodsCount = 0;

                this.wrapperGeneratorMock
                    .Setup(g => g.Generate(It.IsAny<IWrapOptions>(), It.IsAny<Type>(), It.IsAny<IEnumerable<MethodInfo>>()))
                    .Callback<IWrapOptions, Type, IEnumerable<MethodInfo>>((verifyOptions, verifyType, verifyMethods) =>
                    {
                        supportedMethodsCount = verifyMethods.Count();
                    });

                // Act
                this.sut.Wrap(options.Object);

                // Assert
                supportedMethodsCount.ShouldBe(0);
            }

            /// <summary>
            /// When all methods cannot be wrapped then a code file for the type should not created.
            /// </summary>
            [Test]
            public void WhenAllMethodsCannotBeWrappedThenCodeFileShouldBeNotBeCreated()
            {
                // Arrange
                var options = new Mock<IWrapOptions>();
                options.Setup(o => o.AssemblyQualifiedTypeNames).Returns(new string[1]);
                options.Setup(o => o.OutputPath).Returns(OutputPath);

                // Provide a type with a known number of methods
                this.typeMock.Setup(t => t.GetType(It.IsAny<string>())).Returns(typeof(TypeWithKnownMethodCountDummy));

                // All methods cannot be wrapped
                this.methodAnalyserMock.Setup(a => a.CanWrap(It.IsAny<IMethodInfoWrapper>())).Returns(false);

                // Act
                this.sut.Wrap(options.Object);

                // Assert
                this.codeFileWriterMock
                    .Verify(
                        w => w.WriteCodeFileToPath(It.IsAny<string>(), It.IsAny<INamespaceDeclarationSyntaxWrapper>(), It.IsAny<string>()),
                        Times.Never);
            }
        }
    }
}