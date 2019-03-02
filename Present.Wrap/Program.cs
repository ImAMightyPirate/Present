// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.Wrap
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using McMaster.Extensions.CommandLineUtils;
    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Extensions.Logging.Serilog;
    using Present.CodeGeneration.Contracts;
    using Serilog;
    using Serilog.Core;
    using Serilog.Events;

    /// <summary>
    /// Entry point for the Wrap program. The wrap program is a CLI interface
    /// for Present's code generation utilities.
    /// </summary>
    [Command(Name = "wrap", Description = "Utility to create a wrapper class for a .NET type.")]
    public class Program
    {
        /// <summary>
        /// Gets the .NET type to be wrapped.
        /// </summary>
        [Required]
        [Option(ShortName = "t", Description = "The assembly qualified name of the .NET type to be wrapped.")]
        public string Type { get; } = string.Empty;

        /// <summary>
        /// Gets the path where the wrapper class code file should be output.
        /// </summary>
        [Required]
        [Option(ShortName = "o", Description = "The path where the wrapper class code file should be output.")]
        public string Output { get; } = string.Empty;

        /// <summary>
        /// Gets a value indicating whether verbose logging is enabled.
        /// </summary>
        [Option(CommandOptionType.NoValue, ShortName = "v", Description = "Run with verbose logging.")]
        public bool Verbose { get; } = false;

        /// <summary>
        /// Gets a value indicating whether quiet logging is enabled (i.e. logging is turned off).
        /// </summary>
        [Option(CommandOptionType.NoValue, ShortName = "q", Description = "Run with no logging.")]
        public bool Quiet { get; } = false;

        /// <summary>
        /// Initial method called when the program is started. This is routed to
        /// the CommandLineUtils NuGet package to parse the command line arguments,
        /// which then subsequently runs the <see cref="OnExecute"/> method.
        /// </summary>
        /// <param name="args">The command line arguments supplied to the program.</param>
        public static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        /// <summary>
        /// Entry point after the command line utilities parser has run (the properties on
        /// this class should be populated based on the command line arguments supplied).
        /// </summary>
        private void OnExecute()
        {
            // Configure Serilog
            this.ConfigureLogger();

            // Configure Ninject to bind classes and interfaces using naming conventions
            var kernel = new StandardKernel(new SerilogModule());
            kernel.Bind(k => { k.FromAssembliesMatching("*.dll").SelectAllClasses().BindAllInterfaces(); });

            // Perform the wrapping
            var typeWrapper = kernel.Get<ITypeWrapper>();
            typeWrapper.Wrap(this.Type, this.Output);
        }

        /// <summary>
        /// Configures the logger.
        /// </summary>
        private void ConfigureLogger()
        {
            // Default log level is information
            var levelSwitch = new LoggingLevelSwitch { MinimumLevel = LogEventLevel.Information };

            // Only log on error when quiet logging is specified
            if (this.Quiet)
            {
                levelSwitch.MinimumLevel = LogEventLevel.Error;
            }

            // Log everything including debug when verbose logging is specified
            // (verbose intentionally overrides quiet logging)
            if (this.Verbose)
            {
                levelSwitch.MinimumLevel = LogEventLevel.Debug;
            }

            var defaultLogPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Present",
                "Wrap.log");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.Console()
                .WriteTo.File(defaultLogPath)
                .CreateLogger();
        }
    }
}