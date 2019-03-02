// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.DomainObjects
{
    using Present.CodeGeneration.Contracts;

    /// <summary>
    /// Holds options specific for the Wrap program.
    /// </summary>
    public class WrapOptions : IWrapOptions
    {
        /// <summary>
        /// Gets or sets the assembly qualified name of the .NET type to be wrapped.
        /// </summary>
        public string AssemblyQualifiedTypeName { get; set; }

        /// <summary>
        /// Gets or sets the output path of the generated code files.
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether MEF attribute should be emitted.
        /// </summary>
        public bool IncludeMefAttribute { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether MEF2 attribute should be emitted.
        /// </summary>
        public bool IncludeMef2Attribute { get; set; }
    }
}
