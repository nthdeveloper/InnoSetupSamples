using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SampleWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static int Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                List<string> parameters = new List<string>(args);
                if (parameters.Contains("--install"))
                {
                    parameters.Remove("--install");
                    parameters.Add(Assembly.GetExecutingAssembly().Location);
                    try
                    {
                        ManagedInstallerClass.InstallHelper(parameters.ToArray());
                    }
                    catch (Exception e)
                    {
                        return -1;
                    }
                }
                else if (parameters.Contains("--uninstall"))
                {
                    parameters.Remove("--uninstall");
                    parameters.Insert(0, "/u");
                    parameters.Add(Assembly.GetExecutingAssembly().Location);
                    try
                    {
                        ManagedInstallerClass.InstallHelper(parameters.ToArray());
                    }
                    catch (Exception e)
                    {
                        return -1;
                    }
                }
                else
                {
#if DEBUG
                    Console.WriteLine("Starting services...");

                    SampleBackgroundService _backgroundService = new SampleBackgroundService();                    

                    if (!_backgroundService.Start())
                    {
                        Console.WriteLine("Service failed to start.");
                        return -1;
                    }

                    Console.WriteLine("Services started. Press enter to stop...");
                    Console.ReadLine();

                    Console.WriteLine("Stopping service...");
                    _backgroundService.Stop();
                    Console.WriteLine("Stopped.");
#endif
                }
            }
            else
            {
                ServiceBase[] ServicesToRun = new ServiceBase[] { new SampleWindowsService() };
                ServiceBase.Run(ServicesToRun);
            }

            return 0;
        }
    }
}
