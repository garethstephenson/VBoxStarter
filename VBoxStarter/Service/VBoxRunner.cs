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
        private static readonly string VboxManageFilePath = $@"{VboxPath}\VBoxManage.exe";
        private static readonly IEnumerable<string> VmsToRun = Properties.Settings.Default.VMs.Split(',');

        public void Start()
        {
            if (!File.Exists(VboxManageFilePath)) return;

            var listOfRunningVms = GetListOfRunningVms();
            var vmsNotRunning = VmsToRun.Except(listOfRunningVms);

            foreach (var vmToRun in vmsNotRunning)
            {
                var startVMProcessInfo = GetVBoxProcessStartInfo($"startvm \"{vmToRun}\" --type headless");
                var process = Process.Start(startVMProcessInfo);
                process?.WaitForExit();
            }
        }

        public void Stop()
        {

        }

        private static IEnumerable<string> GetListOfRunningVms()
        {
            const int machineNameIndex = 1;

            var listRunningVmsProcessStartInfo = GetVBoxProcessStartInfo("list runningvms", true);
            var runningVmsProcess = Process.Start(listRunningVmsProcessStartInfo);
            var runningVms = new List<string>();

            if (runningVmsProcess == null) return runningVms;

            var runningVmsOutput = runningVmsProcess.StandardOutput.ReadToEnd();
            runningVmsProcess.WaitForExit();

            var matches = Regex.Matches(runningVmsOutput, "\"(.*)\"", RegexOptions.Multiline);
            runningVms.AddRange(matches.Cast<Match>().Select(match => match.Groups[machineNameIndex].Value));
            return runningVms;
        }

        private static ProcessStartInfo GetVBoxProcessStartInfo(string arguments, bool redirectStandardOutput = false, bool useShellExecute = false)
        {
            return new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = VboxManageFilePath,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = redirectStandardOutput,
                Arguments = arguments,
                UseShellExecute = useShellExecute
            };
        }
    }
}