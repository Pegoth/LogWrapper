﻿using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using LogWrapper.Configurations;
using LogWrapper.Enums;
using LogWrapper.Interfaces;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace LogWrapper.Utilities;

public static class LoggerUtils
{
    /// <summary>
    ///     Configures Serilog with the given file.
    ///     <para><b>Warning:</b> Use LogWrapper.Host.LoggingBuilderExtensions.UseLogWrapper in .NET and .NET Core projects.</para>
    /// </summary>
    /// <param name="file">The file to read the configuration from.</param>
    /// <returns>The loaded LogWrapper configuration.</returns>
    public static LogConfiguration Configure(string file = "appsettings.json") => Configure(new ConfigurationBuilder().AddJsonFile(file).Build());

    /// <summary>
    ///     Configures Serilog with the given configuration.
    ///     <para><b>Warning:</b> Use LogWrapper.Host.LoggingBuilderExtensions.UseLogWrapper in .NET and .NET Core projects.</para>
    /// </summary>
    /// <param name="configuration">The configuration to read from.</param>
    /// <returns>The loaded LogWrapper configuration.</returns>
    public static LogConfiguration Configure(IConfiguration configuration)
    {
        // Read the current value
        var wrapperConfig = new LogConfiguration();
        configuration.GetSection("LogSettings").Bind(wrapperConfig);

        return Configure(wrapperConfig, configuration);
    }

    /// <summary>
    ///     Configures Serilog with the given configuration.
    ///     <para><b>Warning:</b> Use LogWrapper.Host.LoggingBuilderExtensions.UseLogWrapper in .NET and .NET Core projects.</para>
    /// </summary>
    /// <param name="wrapperConfig">The configuration to read from.</param>
    /// <returns>The loaded LogWrapper configuration.</returns>
    public static LogConfiguration Configure(LogConfiguration wrapperConfig) => Configure(wrapperConfig, null);

    private static LogConfiguration Configure(LogConfiguration wrapperConfig, IConfiguration? configuration)
    {
        var serilogConfig = new LoggerConfiguration();

        // Setup defaults
        serilogConfig.MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);
        serilogConfig.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);

        wrapperConfig.LogLevel ??= (LogLevels) Enum.Parse(typeof(LogLevels), typeof(LogConfiguration).GetProperty(nameof(LogConfiguration.LogLevel))?.GetCustomAttribute<DefaultValueAttribute>()?.Value?.ToString() ?? nameof(LogLevels.Information));
        wrapperConfig.Sinks    ??= (LogSinks) Enum.Parse(typeof(LogSinks), typeof(LogConfiguration).GetProperty(nameof(LogConfiguration.Sinks))?.GetCustomAttribute<DefaultValueAttribute>()?.Value?.ToString() ?? nameof(LogSinks.Console));

        // Setup serilog based on appsettings.json
        if (configuration != null)
            serilogConfig.ReadFrom.Configuration(configuration);

        // Force load LogWrapper assemblies, (they are not used directly, so they would never be loaded otherwise)
        using var process = Process.GetCurrentProcess();
        var       path    = Path.GetDirectoryName(process.MainModule?.FileName);
        if (!string.IsNullOrEmpty(path))
            foreach (var file in Directory.GetFiles(path, "LogWrapper.Sink.*.dll"))
                AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(file));

        // Setup serilog based on the wrapper configuration
        var t = typeof(ISerilogConfigurator);
        foreach (var configuratorType in AppDomain.CurrentDomain
                                                  .GetAssemblies()
                                                  .SelectMany(assembly => assembly.GetTypes())
                                                  .Where(type => t.IsAssignableFrom(type) && type is {IsInterface: false, IsAbstract: false}))
        {
            var configurator = (ISerilogConfigurator?) Activator.CreateInstance(configuratorType);
            if (configurator is not null && wrapperConfig.Sinks.Value.HasFlag(configurator.Sink))
                configurator.Configure(wrapperConfig, serilogConfig);
        }

        // Configure global minimum log level
        serilogConfig.MinimumLevel.Is(wrapperConfig.LogLevel.Value.ToSerilogLevel());

        // Set the base logger
        Log.Logger = serilogConfig.CreateLogger();

        return wrapperConfig;
    }
}