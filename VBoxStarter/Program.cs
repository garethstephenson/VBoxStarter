using Topshelf;
using VBoxStarter.Service;

namespace VBoxStarter
{
    public static class Program
    {
        public static void Main()
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

                    hostConfigurator.RunAsPrompt();
                    hostConfigurator.StartAutomaticallyDelayed();
                });
        }
    }
}