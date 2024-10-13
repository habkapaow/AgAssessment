using NUnit.Framework;
using System;

namespace UIAutomation.Utilities
{
    public static class BaseUtils
    {
        public static string GetParameter(string parameterName)
        {
            // Fetch the parameter from TestContext
            string parameterValue = TestContext.Parameters.Get(parameterName);

            // If the parameter value is null or empty, throw an exception with the passed name
            if (string.IsNullOrEmpty(parameterValue))
            {
                throw new ArgumentNullException(parameterName, $"{parameterName} cannot be null or empty. Ensure it is defined in the .runsettings file.");
            }

            return parameterValue;
        }
    }
}
