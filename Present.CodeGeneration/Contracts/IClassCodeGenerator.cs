namespace Present.CodeGeneration.Contracts
{
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Contract for the class code generator.
    /// </summary>
    public interface IClassCodeGenerator
    {
        /// <summary>
        /// Generates a Roslyn class definition.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="interfaceName">The name of the interface the type implements.</param>
        /// <returns>The generated interface declaration.</returns>
        ClassDeclarationSyntax Generate(string typeName, string interfaceName);
    }
}