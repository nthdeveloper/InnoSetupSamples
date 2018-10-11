using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace SampleWindowsService
{
    [RunInstaller(true)]
    public partial class SampleServiceInstaller : System.Configuration.Install.Installer
    {
        public const string SERVICE_NAME = "SampleWindowsService";

        private readonly ServiceProcessInstaller serviceProcessInstaller;
        private readonly ServiceInstaller serviceInstaller;

        public SampleServiceInstaller()
        {
            serviceInstaller = new ServiceInstaller();
            serviceInstaller.ServiceName = SERVICE_NAME;
            serviceInstaller.Description = "";
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.DelayedAutoStart = true;

            serviceProcessInstaller = new ServiceProcessInstaller();
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;

            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);

            InitializeComponent();
        }

        /// <summary>
        /// Stops the service before uninstalling
        /// </summary>
        /// <param name="savedState"></param>
        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            ServiceController _serviceController = new ServiceController(SERVICE_NAME);
            try
            {
                _serviceController.Stop();
            }
            catch
            {
            }
            finally
            {
                _serviceController.Dispose();
                base.OnBeforeUninstall(savedState);
            }
        }
    }
}
