// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.Wrap
{
    using System.ComponentModel.DataAnnotations;
    using McMaster.Extensions.CommandLineUtils;
    using Present.CodeGeneration.Contracts;

    /// <summary>
    /// Holds options specific for the Wrap program.
    /// </summary>
    public class WrapOptions : IWrapOptions
    {
        /// <summary>
        /// Gets or sets the assembly qualified name of the .NET types to be wrapped.
        /// </summary>
        [Required]
        [Option(LongName = "type", ShortName = "t", Description = "The assembly qualified name of the .NET types to be wrapped.")]
        public string[] AssemblyQualifiedTypeNames { get; set;  } = new string[0];

        /// <summary>
        /// Gets or sets the output path of the generated code files.
        /// </summary>
        [Required]
        [Option(LongName = "output", ShortName = "o", Description = "The path where the wrapper class code files should be output.")]
        public string OutputPath { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether verbose logging is enabled.
        /// </summary>
        [Option(LongName = "verbose", ShortName = "v", Description = "Run with verbose logging.")]
        public bool UseVerboseLogging { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether quiet logging is enabled (i.e. logging is turned off).
        /// </summary>
        [Option(LongName = "quiet", ShortName = "q", Description = "Run with no logging.")]
        public bool UseQuietLogging { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether MEF export attributes should be emitted.
        /// </summary>
        [Option(LongName = "mef", ShortName = "m", Description = "Decorate generated classes with MEF export attribute (System.ComponentModel.Composition).")]
        public bool IncludeMefAttribute { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether MEF2 export attributes should be emitted.
        /// </summary>
        [Option(LongName = "mef2", ShortName = "m2", Description = "Decorate generated classes with MEF2 export attribute (System.Composition).")]
        public bool IncludeMef2Attribute { get; set; } = false;
    }
}
