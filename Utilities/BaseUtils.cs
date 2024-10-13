using NUnit.Framework;
using System;
using NLog;

namespace AgData.Utilities
{
    public static class BaseUtils
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static string GetParameter(string parameterName)
        {
            Logger.Info($"Fetching parameter: {parameterName}");
            string parameterValue = TestContext.Parameters.Get(parameterName);

            if (string.IsNullOrEmpty(parameterValue))
            {
                Logger.Error($"{parameterName} is null or empty.");
                throw new ArgumentNullException(parameterName, $"{parameterName} cannot be null or empty. Ensure it is defined in the .runsettings file.");
            }

            Logger.Info($"Parameter {parameterName} retrieved successfully.");
            return parameterValue;
        }
    }
}
