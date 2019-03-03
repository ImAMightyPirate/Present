// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.CodeGeneration.Contracts
{
    /// <summary>
    /// Contract for the wrap options.
    /// </summary>
    public interface IWrapOptions
    {
        /// <summary>
        /// Gets or sets the assembly qualified name of the .NET types to be wrapped.
        /// </summary>
        string[] AssemblyQualifiedTypeNames { get; set; }

        /// <summary>
        /// Gets or sets the output path of the generated code files.
        /// </summary>
        string OutputPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether verbose logging is enabled.
        /// </summary>
        bool UseVerboseLogging { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether quiet logging is enabled (i.e. logging is turned off).
        /// </summary>
        bool UseQuietLogging { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether MEF export attributes should be emitted.
        /// </summary>
        bool IncludeMefAttribute { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether MEF2 export attributes should be emitted.
        /// </summary>
        bool IncludeMef2Attribute { get; set; }
    }
}