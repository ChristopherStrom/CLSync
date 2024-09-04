namespace CLSync
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelHost;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelLocalReadDirectory;
        private System.Windows.Forms.Label labelLocalPostDirectory;
        private System.Windows.Forms.Label labelSyncDuration;
        private System.Windows.Forms.Label labelEnableLogging;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtLocalReadDirectory;
        private System.Windows.Forms.TextBox txtLocalPostDirectory;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.CheckBox chkSyncLocal;
        private System.Windows.Forms.CheckBox chkEnableLogging;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox chkSyncRemote;
        private System.Windows.Forms.CheckBox chkPreserveFiles;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.labelHost = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelLocalReadDirectory = new System.Windows.Forms.Label();
            this.labelLocalPostDirectory = new System.Windows.Forms.Label();
            this.labelSyncDuration = new System.Windows.Forms.Label();
            this.labelEnableLogging = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtLocalReadDirectory = new System.Windows.Forms.TextBox();
            this.txtLocalPostDirectory = new System.Windows.Forms.TextBox();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.chkSyncLocal = new System.Windows.Forms.CheckBox();
            this.chkEnableLogging = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.chkSyncRemote = new System.Windows.Forms.CheckBox();
            this.chkPreserveFiles = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelHost
            // 
            this.labelHost.AutoSize = true;
            this.labelHost.Location = new System.Drawing.Point(20, 20);
            this.labelHost.Name = "labelHost";
            this.labelHost.Size = new System.Drawing.Size(32, 13);
            this.labelHost.TabIndex = 0;
            this.labelHost.Text = "Host:";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(20, 50);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(29, 13);
            this.labelPort.TabIndex = 1;
            this.labelPort.Text = "Port:";
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(20, 80);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(58, 13);
            this.labelUsername.TabIndex = 2;
            this.labelUsername.Text = "Username:";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(20, 110);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(56, 13);
            this.labelPassword.TabIndex = 3;
            this.labelPassword.Text = "Password:";
            // 
            // labelLocalReadDirectory
            // 
            this.labelLocalReadDirectory.AutoSize = true;
            this.labelLocalReadDirectory.Location = new System.Drawing.Point(20, 140);
            this.labelLocalReadDirectory.Name = "labelLocalReadDirectory";
            this.labelLocalReadDirectory.Size = new System.Drawing.Size(110, 13);
            this.labelLocalReadDirectory.TabIndex = 4;
            this.labelLocalReadDirectory.Text = "Local Read Directory:";
            // 
            // labelLocalPostDirectory
            // 
            this.labelLocalPostDirectory.AutoSize = true;
            this.labelLocalPostDirectory.Location = new System.Drawing.Point(20, 170);
            this.labelLocalPostDirectory.Name = "labelLocalPostDirectory";
            this.labelLocalPostDirectory.Size = new System.Drawing.Size(105, 13);
            this.labelLocalPostDirectory.TabIndex = 5;
            this.labelLocalPostDirectory.Text = "Local Post Directory:";
            // 
            // labelSyncDuration
            // 
            this.labelSyncDuration.AutoSize = true;
            this.labelSyncDuration.Location = new System.Drawing.Point(20, 200);
            this.labelSyncDuration.Name = "labelSyncDuration";
            this.labelSyncDuration.Size = new System.Drawing.Size(77, 13);
            this.labelSyncDuration.TabIndex = 6;
            this.labelSyncDuration.Text = "Sync Duration:";
            // 
            // labelEnableLogging
            // 
            this.labelEnableLogging.AutoSize = true;
            this.labelEnableLogging.Location = new System.Drawing.Point(20, 230);
            this.labelEnableLogging.Name = "labelEnableLogging";
            this.labelEnableLogging.Size = new System.Drawing.Size(84, 13);
            this.labelEnableLogging.TabIndex = 7;
            this.labelEnableLogging.Text = "Enable Logging:";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(150, 20);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(150, 20);
            this.txtHost.TabIndex = 8;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(150, 50);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(150, 20);
            this.txtPort.TabIndex = 9;
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(150, 80);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(150, 20);
            this.txtUsername.TabIndex = 10;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(150, 110);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(150, 20);
            this.txtPassword.TabIndex = 11;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // txtLocalReadDirectory
            // 
            this.txtLocalReadDirectory.Location = new System.Drawing.Point(150, 140);
            this.txtLocalReadDirectory.Name = "txtLocalReadDirectory";
            this.txtLocalReadDirectory.Size = new System.Drawing.Size(150, 20);
            this.txtLocalReadDirectory.TabIndex = 12;
            // 
            // txtLocalPostDirectory
            // 
            this.txtLocalPostDirectory.Location = new System.Drawing.Point(150, 170);
            this.txtLocalPostDirectory.Name = "txtLocalPostDirectory";
            this.txtLocalPostDirectory.Size = new System.Drawing.Size(150, 20);
            this.txtLocalPostDirectory.TabIndex = 13;
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(150, 200);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(150, 20);
            this.txtDuration.TabIndex = 14;
            // 
            // chkSyncLocal
            // 
            this.chkSyncLocal.AutoSize = true;
            this.chkSyncLocal.Location = new System.Drawing.Point(150, 239);
            this.chkSyncLocal.Name = "chkSyncLocal";
            this.chkSyncLocal.Size = new System.Drawing.Size(79, 17);
            this.chkSyncLocal.TabIndex = 16;
            this.chkSyncLocal.Text = "Sync Local";
            this.chkSyncLocal.UseVisualStyleBackColor = true;
            // 
            // chkEnableLogging
            // 
            this.chkEnableLogging.AutoSize = true;
            this.chkEnableLogging.Location = new System.Drawing.Point(23, 262);
            this.chkEnableLogging.Name = "chkEnableLogging";
            this.chkEnableLogging.Size = new System.Drawing.Size(100, 17);
            this.chkEnableLogging.TabIndex = 17;
            this.chkEnableLogging.Text = "Enable Logging";
            this.chkEnableLogging.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(150, 291);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "Save Settings";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "CLSync";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // chkSyncRemote
            // 
            this.chkSyncRemote.AutoSize = true;
            this.chkSyncRemote.Location = new System.Drawing.Point(23, 239);
            this.chkSyncRemote.Name = "chkSyncRemote";
            this.chkSyncRemote.Size = new System.Drawing.Size(90, 17);
            this.chkSyncRemote.TabIndex = 19;
            this.chkSyncRemote.Text = "Sync Remote";
            this.chkSyncRemote.UseVisualStyleBackColor = true;
            // 
            // chkPreserveFiles
            // 
            this.chkPreserveFiles.AutoSize = true;
            this.chkPreserveFiles.Location = new System.Drawing.Point(150, 262);
            this.chkPreserveFiles.Name = "chkPreserveFiles";
            this.chkPreserveFiles.Size = new System.Drawing.Size(92, 17);
            this.chkPreserveFiles.TabIndex = 20;
            this.chkPreserveFiles.Text = "Preserve Files";
            this.chkPreserveFiles.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(317, 333);
            this.Controls.Add(this.chkPreserveFiles);
            this.Controls.Add(this.chkSyncRemote);
            this.Controls.Add(this.labelHost);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.labelLocalReadDirectory);
            this.Controls.Add(this.txtLocalReadDirectory);
            this.Controls.Add(this.labelLocalPostDirectory);
            this.Controls.Add(this.txtLocalPostDirectory);
            this.Controls.Add(this.labelSyncDuration);
            this.Controls.Add(this.txtDuration);
            this.Controls.Add(this.chkSyncLocal);
            this.Controls.Add(this.chkEnableLogging);
            this.Controls.Add(this.btnSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "CLSync Configuration";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
