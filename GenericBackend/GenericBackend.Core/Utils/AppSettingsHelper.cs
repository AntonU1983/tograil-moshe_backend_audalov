using System.Collections.Concurrent;
using System.Configuration;

namespace GenericBackend.Core.Utils
{
    public class AppSettingsHelper
    {
        private static ConcurrentDictionary<string, string> _appSettings = new ConcurrentDictionary<string, string>();
        public static string StorageConnectionString { get { return GetOrAddAppSettings("StorageConnectionString"); } }
        
        public static string GetOrAddAppSettings(string key)
        {
            var value = _appSettings.GetOrAdd(key, v => ConfigurationManager.AppSettings[key]);

            return value;
        }

    }
}