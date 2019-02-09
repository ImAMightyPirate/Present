namespace Present.CodeGeneration.Generators
{
    using Contracts;
    using EnsureThat;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Responsible for generating the Roslyn definition for an interface.
    /// </summary>
    public class InterfaceCodeGenerator : IInterfaceCodeGenerator
    {
        /// <summary>
        /// Generates a Roslyn interface definition.
        /// </summary>
        /// <param name="interfaceName">The interface name.</param>
        /// <returns>The generated interface declaration.</returns>
        public InterfaceDeclarationSyntax Generate(string interfaceName)
        {
            Ensure.That(interfaceName).IsNotNullOrWhiteSpace();

            // Create a public interface
            return SyntaxFactory
                .InterfaceDeclaration(interfaceName)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
        }
    }
}
