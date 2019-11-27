using Microsoft.Extensions.Configuration;

namespace FIleAPI_CLI
{
    public sealed class ConfigurationSingleton
    {
        private static ConfigurationSingleton _instance = null;

        public IConfigurationRoot configuration;
        public IConfigurationRoot inputParameters;

        private ConfigurationSingleton()
        {
            configuration = new ConfigurationBuilder()
                            .AddJsonFile("appSettings.json", optional: false)
                            .Build();

            inputParameters = new ConfigurationBuilder()
                            .AddJsonFile("inputData.json", optional: false)
                            .Build();
        }

        public static ConfigurationSingleton Settings
        {
            get
            {
                if (_instance == null) _instance = new ConfigurationSingleton();
                return _instance;
            }
        }
    }
}
