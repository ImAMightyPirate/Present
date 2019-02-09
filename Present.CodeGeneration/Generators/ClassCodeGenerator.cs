namespace Present.CodeGeneration.Generators
{
    using Contracts;
    using EnsureThat;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Responsible for generating the Roslyn definition for a class.
    /// </summary>
    public class ClassCodeGenerator : IClassCodeGenerator
    {
        /// <summary>
        /// Generates a Roslyn class definition.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="interfaceName">The name of the interface the type implements.</param>
        /// <returns>The generated interface declaration.</returns>
        public ClassDeclarationSyntax Generate(string typeName, string interfaceName)
        {
            Ensure.That(typeName).IsNotNullOrWhiteSpace();
            Ensure.That(interfaceName).IsNotNullOrWhiteSpace();

            // Create a public sealed class that implements an interface
            return SyntaxFactory
                .ClassDeclaration(typeName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.SealedKeyword))
                .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.IdentifierName(interfaceName)));
        }
    }
}
