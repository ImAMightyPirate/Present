namespace Present.CodeGeneration.Contracts
{
    using System.Reflection;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Contract for the method code generator.
    /// </summary>
    public interface IMethodCodeGenerator
    {
        /// <summary>
        /// Generates a Roslyn method definition from a <see cref="MethodInfo"/> object.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The generated method declaration and method body.</returns>
        (MethodDeclarationSyntax methodDeclaration, BlockSyntax methodBody) Generate(MethodInfo method);
    }
}