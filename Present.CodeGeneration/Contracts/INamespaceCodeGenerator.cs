namespace Present.CodeGeneration.Contracts
{
    using System.Collections.Generic;
    using System.Reflection;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Contract for the namespace generator.
    /// </summary>
    public interface INamespaceCodeGenerator
    {
        /// <summary>
        /// Generates a Roslyn namespace definition from a type and its methods.
        /// </summary>
        /// <param name="typeNamespace">The namespace which the type belongs to.</param>
        /// <param name="typeName">The name of the type.</param>
        /// <param name="methods">The methods to be wrapped.</param>
        /// <returns>The generated namespace declaration.</returns>
        NamespaceDeclarationSyntax Generate(string typeNamespace, string typeName, IEnumerable<MethodInfo> methods);
    }
}