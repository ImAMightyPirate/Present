// Copyright (c) Present.NET. All Rights Reserved.
// Present.NET is licensed under the MIT License. For usage and redistribution terms please refer to the LICENSE file.
// For more information about the Present.NET project visit https://github.com/ImAMightyPirate/Present.

namespace Present.Wrap
{
    using System;
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
    public class Program : WrapOptions
    {
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
            var wrappedTypeBuilder = kernel.Get<IWrappedTypeBuilder>();
            wrappedTypeBuilder.Wrap(this);
        }

        /// <summary>
        /// Configures the logger.
        /// </summary>
        private void ConfigureLogger()
        {
            // Default log level is information
            var levelSwitch = new LoggingLevelSwitch { MinimumLevel = LogEventLevel.Information };

            // Only log on error when quiet logging is specified
            if (this.UseQuietLogging)
            {
                levelSwitch.MinimumLevel = LogEventLevel.Error;
            }

            // Log everything including debug when verbose logging is specified
            // (verbose intentionally overrides quiet logging)
            if (this.UseVerboseLogging)
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