using System;
using System.IO;
using System.Xml.Serialization;

namespace CLSync
{
    public class Config
    {
        public SftpSettings SftpSettings { get; set; }
        public string LocalReadDirectory { get; set; }
        public string LocalPostDirectory { get; set; }
        public int SyncDuration { get; set; } // in seconds
        public bool SyncRemote { get; set; }
        public bool SyncLocal { get; set; }
        public bool EnableLogging { get; set; }
        public bool PreserveFiles { get; set; }
    }
}
