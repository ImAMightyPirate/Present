namespace Present.CodeGeneration.Contracts
{
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Contract for the interface code generator.
    /// </summary>
    public interface IInterfaceCodeGenerator
    {
        /// <summary>
        /// Generates a Roslyn interface definition.
        /// </summary>
        /// <param name="interfaceName">The interface name.</param>
        /// <returns>The generated interface declaration.</returns>
        InterfaceDeclarationSyntax Generate(string interfaceName);
    }
}