using System.IO;
using System.Xml.Serialization;

namespace CLSync
{
    public class ConfigManager
    {
        private readonly string _configFilePath;

        public ConfigManager(string configFilePath)
        {
            _configFilePath = configFilePath;
        }

        public Config Load()
        {
            if (!File.Exists(_configFilePath))
            {
                return new Config
                {
                    SftpSettings = new SftpSettings(),
                    LocalReadDirectory = @"C:\CLSync\Read",
                    LocalPostDirectory = @"C:\CLSync\Post",
                    SyncDuration = 60,
                    SyncRemote = true,
                    SyncLocal = true,
                    EnableLogging = true
                };
            }

            var serializer = new XmlSerializer(typeof(Config));
            using (var reader = new StreamReader(_configFilePath))
            {
                return (Config)serializer.Deserialize(reader);
            }
        }

        public void Save(Config config)
        {
            var serializer = new XmlSerializer(typeof(Config));
            using (var writer = new StreamWriter(_configFilePath))
            {
                serializer.Serialize(writer, config);
            }
        }
    }
}
