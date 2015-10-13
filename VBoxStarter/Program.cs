using System.Diagnostics;
using System.IO;
using System.Linq;
using Topshelf;

namespace VBoxStarter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            HostFactory
                .Run(hostConfigurator =>
                {
                    hostConfigurator.Service<IVBoxRunner>(serviceConfigurator =>
                    {
                        serviceConfigurator.ConstructUsing(() => new VBoxRunner());
                        serviceConfigurator.WhenStarted(vBoxRunner => vBoxRunner.Start());
                        serviceConfigurator.WhenStopped(vBoxRunner => vBoxRunner.Stop());
                    });

                    hostConfigurator.SetServiceName("VBoxStarterService");
                    hostConfigurator.SetDisplayName("VBox Starter Service");
                    hostConfigurator.SetDescription("A Windows Service to start VirtualBox instances in headless mode, on start up");

                    hostConfigurator.RunAsNetworkService();
                    hostConfigurator.StartAutomaticallyDelayed();
                });
        }
    }

    internal class VBoxRunner : IVBoxRunner
    {
        public void Start()
        {
            if (Directory.Exists(Properties.Settings.Default.VirtualBoxPath))
            {
                if (File.Exists($@"{Properties.Settings.Default.VirtualBoxPath}\VBoxManage.exe"))
                {
                    var vmNames = Properties.Settings.Default.VMs.Split(',');
                    foreach (var vmName in vmNames)
                    {

                        var processStartInfo = new ProcessStartInfo
                        {
                            CreateNoWindow = true,
                            FileName = $@"{Properties.Settings.Default.VirtualBoxPath}\VBoxManage.exe",
                            Arguments = $"startvm \"{vmName}\" --type headless",
                            WindowStyle = ProcessWindowStyle.Hidden
                        };
                        Process.Start(processStartInfo);
                    }
                }
            }
        }

        public void Stop()
        {

        }
    }

    internal interface IVBoxRunner
    {
        void Start();

        void Stop();
    }
}
