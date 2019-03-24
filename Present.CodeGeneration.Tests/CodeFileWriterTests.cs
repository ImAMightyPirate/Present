// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Moq;
    using NUnit.Framework;
    using Present.CodeGeneration.Wrappers.Custom;
    using Present.IO;
    using Shouldly;

    using ILogger = Ninject.Extensions.Logging.ILogger;

    /// <summary>
    /// Unit tests that cover the behaviour of the <see cref="CodeFileWriter"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class CodeFileWriterTests
    {
        private const string TypeName = "Type1";
        private const string OutputPath = @"C:\Some\Path";
        private const string GeneratedCode = "Hello World";

        private CodeFileWriter sut;

        private Mock<ILogger> loggerMock;
        private Mock<IDirectory> directoryMock;
        private Mock<IFile> fileMock;
        private Mock<IPath> pathMock;

        /// <summary>
        /// Set up run prior to each test.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.loggerMock = new Mock<ILogger>();
            this.directoryMock = new Mock<IDirectory>();
            this.fileMock = new Mock<IFile>();
            this.pathMock = new Mock<IPath>();

            this.sut = new CodeFileWriter(
                this.loggerMock.Object,
                this.directoryMock.Object,
                this.fileMock.Object,
                this.pathMock.Object);
        }

        /// <summary>
        /// Unit tests that cover the behaviour of the <see cref="CodeFileWriter.WriteCodeFileToPath"/> method.
        /// </summary>
        [TestFixture]
        public class WriteCodeFileToPath : CodeFileWriterTests
        {
            /// <summary>
            /// When the type name argument is invalid then a <see cref="ArgumentException"/> should be thrown.
            /// </summary>
            /// <param name="typeName">The type name argument value.</param>
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            public void WhenTypeNameArgumentIsInvalidThenArgumentExceptionShouldBeThrown(string typeName)
            {
                // Act + Assert
                Should.Throw<ArgumentException>(() => this.sut.WriteCodeFileToPath(
                    typeName,
                    new Mock<INamespaceDeclarationSyntaxWrapper>().Object,
                    OutputPath));
            }

            /// <summary>
            /// When the output path argument is null then a <see cref="ArgumentNullException"/> should be thrown.
            /// </summary>
            [Test]
            public void WhenNamespaceDeclarationArgumentIsNullThenArgumentNullExceptionShouldBeThrown()
            {
                // Act + Assert
                Should.Throw<ArgumentNullException>(() => this.sut.WriteCodeFileToPath(
                    TypeName,
                    null,
                    OutputPath));
            }

            /// <summary>
            /// When the output path argument is invalid then a <see cref="ArgumentException"/> should be thrown.
            /// </summary>
            /// <param name="outputPath">The output path argument value.</param>
            [TestCase(null)]
            [TestCase("")]
            [TestCase(" ")]
            public void WhenOutputPathArgumentIsInvalidThenArgumentExceptionShouldBeThrown(string outputPath)
            {
                // Act + Assert
                Should.Throw<ArgumentException>(() => this.sut.WriteCodeFileToPath(
                    TypeName,
                    new Mock<INamespaceDeclarationSyntaxWrapper>().Object,
                    outputPath));
            }

            /// <summary>
            /// When the output path does not already exist then the directory should be created.
            /// </summary>
            [Test]
            public void WhenOutputPathDoesNotAlreadyExistThenDirectoryShouldBeCreated()
            {
                // Arrange
                this.directoryMock
                    .Setup(d => d.Exists(It.IsAny<string>()))
                    .Returns(false);

                // Act
                this.sut.WriteCodeFileToPath(
                    TypeName,
                    new Mock<INamespaceDeclarationSyntaxWrapper>().Object,
                    OutputPath);

                // Assert
                this.directoryMock
                    .Verify(d => d.CreateDirectory(OutputPath));
            }

            /// <summary>
            /// When the output path already exists then the directory should not be created.
            /// </summary>
            [Test]
            public void WhenOutputPathAlreadyExistsThenDirectoryShouldNotBeCreated()
            {
                // Arrange
                this.directoryMock
                    .Setup(d => d.Exists(It.IsAny<string>()))
                    .Returns(true);

                // Act
                this.sut.WriteCodeFileToPath(
                    TypeName,
                    new Mock<INamespaceDeclarationSyntaxWrapper>().Object,
                    OutputPath);

                // Assert
                this.directoryMock
                    .Verify(d => d.CreateDirectory(OutputPath), Times.Never);
            }

            /// <summary>
            /// When a valid namespace declaration is supplied then then generated code should be written to a file.
            /// </summary>
            [Test]
            public void WhenNamespaceDeclarationSuppliedThenGeneratedCodeShouldBeWrittenToFile()
            {
                // Arrange
                var namespaceDeclarationSyntaxMock = new Mock<INamespaceDeclarationSyntaxWrapper>();
                namespaceDeclarationSyntaxMock
                    .Setup(n => n.ToString())
                    .Returns(GeneratedCode);

                // Act
                this.sut.WriteCodeFileToPath(
                    TypeName,
                    namespaceDeclarationSyntaxMock.Object,
                    OutputPath);

                // Assert
                this.fileMock
                    .Verify(f => f.WriteAllText(It.IsAny<string>(), GeneratedCode, Encoding.UTF8));
            }
        }
    }
}