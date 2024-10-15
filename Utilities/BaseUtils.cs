using log4net;
using log4net.Config;
using NUnit.Framework;
using System;
using System.IO;

namespace AgData.Utilities
{
    public static class BaseUtils
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(BaseUtils));
        private static bool isLoggerInitialized = false;

        // Initialize logger with the config file
        public static void InitializeLogger()
        {
            if (!isLoggerInitialized)
            {
                var repository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());

                // Check if the logger repository is already configured
                if (repository == null)
                {
                    throw new InvalidOperationException("Logger repository not found. Cannot configure log4net.");
                }

                // Ensure that the configuration file exists
                var logConfigFile = new FileInfo("log4net.config");
                if (!logConfigFile.Exists)
                {
                    throw new FileNotFoundException("Log4net configuration file not found: log4net.config");
                }

                // Explicitly configure log4net to use the log4net.config file
                XmlConfigurator.Configure(repository, logConfigFile);

                log.Info("Logger initialized.");
                isLoggerInitialized = true;  // Ensure the logger is only initialized once
            }
        }

        public static string GetParameter(string parameterName)
        {
            // Ensure the logger is initialized
            InitializeLogger();

            // Fetch the parameter from TestContext
            string parameterValue = TestContext.Parameters.Get(parameterName);

            // If the parameter value is null or empty, log and throw an exception with the passed name
            if (string.IsNullOrEmpty(parameterValue))
            {
                log.Error($"{parameterName} is null or empty.");
                throw new ArgumentNullException(parameterName, $"{parameterName} cannot be null or empty. Ensure it is defined in the .runsettings file.");
            }

            log.Info($"Successfully fetched parameter: {parameterName} = {parameterValue}");
            return parameterValue;
        }
    }
}
