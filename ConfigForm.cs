using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Renci.SshNet;

namespace CLSync
{
    public partial class ConfigForm : Form
    {
        private Config _config;
        private readonly string baseDirectory = @"c:\CLSync\";
        private readonly string logFilePath;
        private readonly string configFilePath;
        private Timer syncTimer;
        private int timeRemaining;

        public ConfigForm()
        {
            // Ensure base directory exists
            Directory.CreateDirectory(baseDirectory);

            // Set file paths
            logFilePath = Path.Combine(baseDirectory, "sync.log");
            configFilePath = Path.Combine(baseDirectory, "settings.config");

            InitializeComponent();
            LoadConfig();
            InitializeTimer();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipText = "SFTP Sync is running in the background.";
            notifyIcon1.ShowBalloonTip(1000);
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
        }

        private void InitializeTimer()
        {
            timeRemaining = _config.SyncDuration;
            syncTimer = new Timer();
            syncTimer.Interval = 1000; // 1 second
            syncTimer.Tick += SyncTimer_Tick;
            syncTimer.Start();
        }

        private void SyncTimer_Tick(object sender, EventArgs e)
        {
            timeRemaining--;

            if (timeRemaining <= 0)
            {
                SyncSftp();
                timeRemaining = _config.SyncDuration; // Reset the timer
            }

            notifyIcon1.Text = $"SFTP Sync Service - Next sync in {timeRemaining} seconds";
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            _config = Config.Load(configFilePath);

            txtHost.Text = _config.SftpSettings.Host;
            txtPort.Text = _config.SftpSettings.Port.ToString();
            txtUsername.Text = _config.SftpSettings.Username;
            txtPassword.Text = _config.SftpSettings.Password;
            txtLocalDirectory.Text = _config.LocalDirectory;
            txtRemoteDirectory.Text = _config.RemoteDirectory;
            txtSyncDuration.Text = _config.SyncDuration.ToString();
        }

        private void SaveConfig()
        {
            _config.SftpSettings.Host = txtHost.Text;
            _config.SftpSettings.Port = int.Parse(txtPort.Text);
            _config.SftpSettings.Username = txtUsername.Text;
            _config.SftpSettings.Password = txtPassword.Text;
            _config.LocalDirectory = txtLocalDirectory.Text;
            _config.RemoteDirectory = txtRemoteDirectory.Text;
            _config.SyncDuration = int.Parse(txtSyncDuration.Text);

            _config.Save(configFilePath);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveConfig();
            MessageBox.Show("Configuration saved. The application will restart.");
            Application.Restart();
        }

        private string CalculateFileHash(Stream fileStream)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(fileStream);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private void SyncSftp()
        {
            try
            {
                LogToFile("Starting SFTP sync...");
                using (var sftp = new SftpClient(_config.SftpSettings.Host, _config.SftpSettings.Port, _config.SftpSettings.Username, _config.SftpSettings.Password))
                {
                    sftp.Connect();
                    LogToFile("Connected to SFTP server.");

                    SyncDirectory(sftp, _config.LocalDirectory, _config.RemoteDirectory);

                    sftp.Disconnect();
                    LogToFile("Disconnected from SFTP server.");
                }
            }
            catch (Exception ex)
            {
                LogToFile($"Error during SFTP sync: {ex.Message} {ex.StackTrace}");
            }
        }

        private void SyncDirectory(SftpClient sftp, string localDirectory, string remoteDirectory)
        {
            // Ensure remote directory exists
            if (!sftp.Exists(remoteDirectory))
            {
                sftp.CreateDirectory(remoteDirectory);
            }

            // Sync from remote to local
            var remoteFiles = sftp.ListDirectory(remoteDirectory);
            foreach (var remoteFile in remoteFiles)
            {
                if (remoteFile.IsDirectory)
                {
                    if (remoteFile.Name != "." && remoteFile.Name != "..")
                    {
                        string localSubDirectory = Path.Combine(localDirectory, remoteFile.Name);
                        string remoteSubDirectory = remoteFile.FullName;
                        SyncDirectory(sftp, localSubDirectory, remoteSubDirectory);
                    }
                }
                else
                {
                    string localFilePath = Path.Combine(localDirectory, remoteFile.Name);
                    if (!File.Exists(localFilePath) || remoteFile.LastWriteTime > File.GetLastWriteTime(localFilePath))
                    {
                        using (Stream fileStream = File.Create(localFilePath))
                        {
                            sftp.DownloadFile(remoteFile.FullName, fileStream);
                        }
                        LogToFile($"Downloaded file: {remoteFile.Name}");
                    }
                }
            }

            // Sync from local to remote
            var localFiles = Directory.GetFiles(localDirectory);
            foreach (var localFilePath in localFiles)
            {
                var fileName = Path.GetFileName(localFilePath);
                var remoteFilePath = Path.Combine(remoteDirectory, fileName).Replace("\\", "/");
                try
                {
                    var remoteFileAttributes = sftp.GetAttributes(remoteFilePath);
                    LogToFile($"Remote file {remoteFilePath} exists. Checking if it needs to be updated.");

                    // Compare file hashes
                    using (var localFileStream = new FileStream(localFilePath, FileMode.Open, FileAccess.Read))
                    {
                        var localFileHash = CalculateFileHash(localFileStream);
                        var remoteFileStream = sftp.OpenRead(remoteFilePath);
                        var remoteFileHash = CalculateFileHash(remoteFileStream);
                        remoteFileStream.Dispose();

                        if (localFileHash != remoteFileHash)
                        {
                            using (var fileStream = new FileStream(localFilePath, FileMode.Open))
                            {
                                sftp.UploadFile(fileStream, remoteFilePath, true);
                            }
                            LogToFile($"Uploaded updated file: {fileName} to {remoteFilePath}");
                        }
                        else
                        {
                            LogToFile($"Skipped uploading {fileName} as it is identical to the remote file.");
                        }
                    }
                }
                catch (Renci.SshNet.Common.SftpPathNotFoundException)
                {
                    LogToFile($"Remote file {remoteFilePath} not found. Uploading new file.");
                    using (var fileStream = new FileStream(localFilePath, FileMode.Open))
                    {
                        sftp.UploadFile(fileStream, remoteFilePath);
                    }
                    LogToFile($"Uploaded new file: {fileName} to {remoteFilePath}");
                }
                catch (Exception ex)
                {
                    LogToFile($"Error uploading file {fileName} to {remoteFilePath}: {ex.Message}");
                }
            }

            // Delete remote files that do not exist locally
            foreach (var remoteFile in remoteFiles)
            {
                if (!remoteFile.IsDirectory)
                {
                    string localFilePath = Path.Combine(localDirectory, remoteFile.Name);
                    if (!File.Exists(localFilePath))
                    {
                        sftp.DeleteFile(remoteFile.FullName);
                        LogToFile($"Deleted remote file: {remoteFile.FullName}");
                    }
                }
            }
        }

        private void LogToFile(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("SftpSyncServiceSource", $"Failed to write to log file: {ex.Message}");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
