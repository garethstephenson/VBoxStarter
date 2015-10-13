using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace VBoxStarter.Service
{
    internal class VBoxRunner : IVBoxRunner
    {
        private static readonly string VboxPath = Properties.Settings.Default.VirtualBoxPath;
        private static readonly string VboxManageFilePath = $@"{Properties.Settings.Default.VirtualBoxPath}\VBoxManage.exe";

        public void Start()
        {
            if (Directory.Exists(VboxPath) && File.Exists(VboxManageFilePath))
            {
                var listOfRunningVms = GetListOfRunningVms();
                var vmsToRun = Properties.Settings.Default.VMs.Split(',');

                foreach (var vmToRun in vmsToRun)
                {
                    if (!listOfRunningVms.Contains(vmToRun))
                    {
                        var startVMProcessInfo = new ProcessStartInfo
                        {
                            CreateNoWindow = true,
                            FileName = VboxManageFilePath,
                            Arguments = $"startvm \"{vmToRun}\" --type headless",
                            WindowStyle = ProcessWindowStyle.Hidden
                        };
                        Process.Start(startVMProcessInfo);
                    }
                }
            }
        }

        public void Stop()
        {

        }

        private static List<string> GetListOfRunningVms()
        {
            var listRunningVMProcessInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = VboxManageFilePath,
                Arguments = $"list runningvms",
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            var runningVms = new List<string>();
            var runningVmsProcess = Process.Start(listRunningVMProcessInfo);
            if (runningVmsProcess != null)
            {
                var runningVmsOutput = runningVmsProcess.StandardOutput.ReadToEnd();
                runningVmsProcess.WaitForExit();

                var regex = new Regex("\".*\"", RegexOptions.Multiline);
                var matches = regex.Matches(runningVmsOutput);
                runningVms.AddRange((matches.Cast<Match>().Select(match => match.Value.Replace("\"", string.Empty))));
            }
            return runningVms;
        }
    }
}