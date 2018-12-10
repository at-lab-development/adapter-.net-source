using System.Configuration;

namespace TestProject.SpecFlow.Common
{
    /// <summary>
    /// Class for configuration App.config
    /// </summary>
    public class Configuration
    {
        public static readonly string StartUrlValue = ConfigurationManager.AppSettings["StartUrl"];
        public static readonly string BrowserValue = ConfigurationManager.AppSettings["Browser"];
        public static readonly string ElementTimeoutValue = ConfigurationManager.AppSettings["ElementTimeout"];

        public static string GetEnvironmentVar(string var, string defaultValue)
        {
            return ConfigurationManager.AppSettings[var] ?? defaultValue;
        }

        public static string ElementTimeout => GetEnvironmentVar("ElementTimeout", ElementTimeoutValue);

        public static string Browser => GetEnvironmentVar("Browser", BrowserValue);

        public static string StartUrl => GetEnvironmentVar("StartUrl", StartUrlValue);
    }
}