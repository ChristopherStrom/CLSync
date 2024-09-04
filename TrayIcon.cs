using System;
using System.Drawing;
using System.Windows.Forms;

namespace CLSync
{
    public class TrayIcon : IDisposable
    {
        private NotifyIcon _notifyIcon;
        private MainForm _mainForm;

        public TrayIcon(MainForm mainForm)
        {
            _mainForm = mainForm;

            _notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application,
                ContextMenuStrip = new ContextMenuStrip(),
                Text = "SFTP Sync",
                Visible = true
            };

            _notifyIcon.ContextMenuStrip.Items.Add("Settings", null, ShowSettings);
            _notifyIcon.ContextMenuStrip.Items.Add("Exit", null, Exit);

            _notifyIcon.DoubleClick += ShowSettings;
        }

        private void ShowSettings(object sender, EventArgs e)
        {
            _mainForm.WindowState = FormWindowState.Normal;
            _mainForm.ShowInTaskbar = true;
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void Dispose()
        {
            _notifyIcon.Dispose();
        }
    }
}
