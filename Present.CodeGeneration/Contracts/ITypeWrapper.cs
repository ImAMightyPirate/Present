namespace Present.CodeGeneration.Contracts
{
    /// <summary>
    /// Contract for the type wrapper.
    /// </summary>
    public interface ITypeWrapper
    {
        /// <summary>
        /// Creates a wrapper class for a .NET type and outputs
        /// the code file.
        /// </summary>
        /// <param name="assemblyQualifiedTypeName">The assembly qualified name of the .NET type to be wrapped.</param>
        /// <param name="outputPath">The output path of the generated code file.</param>
        void Wrap(string assemblyQualifiedTypeName, string outputPath);
    }
}