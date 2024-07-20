using System;
using System.IO;
using System.Xml.Serialization;

namespace CLSync
{
    public class SftpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class Config
    {
        public SftpSettings SftpSettings { get; set; }
        public string LocalDirectory { get; set; }
        public string RemoteDirectory { get; set; }
        public int SyncDuration { get; set; } // Sync duration in seconds

        public static Config Load(string configPath)
        {
            if (!File.Exists(configPath))
            {
                return new Config
                {
                    SftpSettings = new SftpSettings
                    {
                        Host = "your_sftp_server",
                        Port = 22,
                        Username = "your_username",
                        Password = "your_password"
                    },
                    LocalDirectory = @"C:\CLSync\data",
                    RemoteDirectory = "/remote/directory",
                    SyncDuration = 600 // Default sync duration
                };
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            using (FileStream stream = new FileStream(configPath, FileMode.Open))
            {
                return (Config)serializer.Deserialize(stream);
            }
        }

        public void Save(string configPath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Config));
            using (FileStream stream = new FileStream(configPath, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }
    }
}
