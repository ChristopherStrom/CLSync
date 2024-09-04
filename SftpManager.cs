using System;
using System.Diagnostics;
using System.IO;

namespace CLSync
{
    public class SftpManager
    {
        public void SyncWithSftp(Config config, string syncDirection)
        {
            string scriptContent = GenerateScriptContent(config, syncDirection);

            // Write the script to the lib directory
            string scriptPath = Path.Combine(@"C:\CLSync\lib\", "winscp_script.txt");
            File.WriteAllText(scriptPath, scriptContent);

            // Command to execute WinSCP with the script
            string command = $"\"C:\\CLSync\\lib\\WinSCP.exe\" /script=\"{scriptPath}\"";

            LogMessage($"Executing command: {command}");
            ExecuteCommand(command);
        }

        private string GenerateScriptContent(Config config, string syncDirection)
        {
            string deleteOption = config.PreserveFiles ? "" : " -delete";
            string overwriteOption = config.PreserveFiles ? "" : " -resume -neweronly";

            string readDirectory = Path.GetDirectoryName(config.LocalReadDirectory);
            string readFilePattern = Path.GetFileName(config.LocalReadDirectory);

            string postDirectory = Path.GetDirectoryName(config.LocalPostDirectory);
            string postFileName = Path.GetFileName(config.LocalPostDirectory);

            string script = $@"
open sftp://{config.SftpSettings.Username}:{config.SftpSettings.Password}@{config.SftpSettings.Host}:{config.SftpSettings.Port}
option batch abort
option confirm off
";

            if (syncDirection == "both" || syncDirection == "remote")
            {
                // Sync remote files to local read directory
                script += $@"
lcd ""{readDirectory}""
get ""/{readFilePattern}""{deleteOption}{overwriteOption}";
            }

            if (syncDirection == "both" || syncDirection == "local")
            {
                // Sync local files from post directory to remote
                script += $@"
lcd ""{postDirectory}""
put ""{postFileName}"" ""/""{deleteOption}{overwriteOption}";
            }

            script += @"
exit";
            return script;
        }

        private void ExecuteCommand(string command)
        {
            try
            {
                string fullCommand = $"& {command}";
                LogMessage($"Executing command via PowerShell: {fullCommand}");

                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-Command \"{fullCommand}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    WorkingDirectory = @"C:\CLSync\lib\"
                };

                using (Process process = Process.Start(processInfo))
                {
                    process.WaitForExit();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    if (!string.IsNullOrEmpty(output))
                    {
                        LogMessage($"Output: {output}");
                    }

                    if (!string.IsNullOrEmpty(error))
                    {
                        LogMessage($"Error: {error}");
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage($"Exception in ExecuteCommand: {ex.Message}");
            }
        }

        private void LogMessage(string message)
        {
            string logFilePath = Path.Combine(@"C:\CLSync\logs\sftp_log.txt");
            File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
        }
    }
}
