using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CLSync
{
    public partial class MainForm : Form
    {
        private ConfigManager configManager;
        private SftpManager sftpManager;
        private readonly string configFilePath = @"C:\CLSync\settings.xml"; // Define configFilePath here
        private Timer syncTimer;

        public MainForm()
        {
            InitializeComponent();
            configManager = new ConfigManager(configFilePath);
            sftpManager = new SftpManager();

            // Set up the timer
            syncTimer = new Timer();
            syncTimer.Tick += SyncTimer_Tick;

            Log("MainForm initialized.");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Log("MainForm loaded.");
            LoadConfig();
            MinimizeToTray();
        }

        private void LoadConfig()
        {
            Log("Loading configuration...");
            Config config = configManager.Load();

            if (config != null)
            {
                txtHost.Text = config.SftpSettings.Host;
                txtPort.Text = config.SftpSettings.Port.ToString();
                txtUsername.Text = config.SftpSettings.Username;
                txtPassword.Text = config.SftpSettings.Password;
                txtLocalReadDirectory.Text = config.LocalReadDirectory;
                txtLocalPostDirectory.Text = config.LocalPostDirectory;
                txtDuration.Text = config.SyncDuration.ToString();
                chkSyncRemote.Checked = config.SyncRemote;
                chkSyncLocal.Checked = config.SyncLocal;
                chkEnableLogging.Checked = config.EnableLogging;
                chkPreserveFiles.Checked = config.PreserveFiles;

                Log("Configuration loaded successfully.");

                // Set up the sync timer
                syncTimer.Interval = config.SyncDuration * 1000; // Duration is in seconds, timer uses milliseconds
                syncTimer.Start();
            }
            else
            {
                Log("Failed to load configuration.");
            }
        }

        private void SyncTimer_Tick(object sender, EventArgs e)
        {
            Log("SyncTimer_Tick triggered.");
            var config = configManager.Load();
            SyncWithSftp(config);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Log("Save button clicked.");
                var config = new Config
                {
                    SftpSettings = new SftpSettings
                    {
                        Host = txtHost.Text,
                        Port = int.Parse(txtPort.Text),
                        Username = txtUsername.Text,
                        Password = txtPassword.Text
                    },
                    LocalReadDirectory = txtLocalReadDirectory.Text,
                    LocalPostDirectory = txtLocalPostDirectory.Text,
                    SyncDuration = int.Parse(txtDuration.Text),
                    SyncRemote = chkSyncRemote.Checked,
                    SyncLocal = chkSyncLocal.Checked,
                    EnableLogging = chkEnableLogging.Checked,
                    PreserveFiles = chkPreserveFiles.Checked
                };

                configManager.Save(config);
                Log("Configuration saved. Attempting to sync...");

                // Restart the timer with the new interval
                syncTimer.Interval = config.SyncDuration * 1000; // Duration is in seconds, timer uses milliseconds
                syncTimer.Start();

                SyncWithSftp(config);
                MinimizeToTray();
            }
            catch (Exception ex)
            {
                Log($"Exception in btnSave_Click: {ex.Message}");
            }
        }

        private void SyncWithSftp(Config config)
        {
            try
            {
                Log("Starting sync with SFTP...");
                string syncDirection;

                if (config.SyncRemote && config.SyncLocal)
                {
                    syncDirection = "both";
                }
                else if (config.SyncRemote)
                {
                    syncDirection = "remote";
                }
                else
                {
                    syncDirection = "local";
                }

                sftpManager.SyncWithSftp(config, syncDirection);
                Log("SyncWithSftp completed.");
            }
            catch (Exception ex)
            {
                Log($"Exception in SyncWithSftp: {ex.Message}");
            }
        }

        private void MinimizeToTray()
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipText = "SFTP Sync is running in the background.";
            notifyIcon1.ShowBalloonTip(1000);
            Log("Application minimized to tray.");
        }

        private void Log(string message)
        {
            if (!chkEnableLogging.Checked) return;

            try
            {
                string logFilePath = @"C:\CLSync\logs\sftp_log.txt";
                using (var writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to write to log: {ex.Message}");
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            Log("Application restored from tray.");
        }
    }
}
