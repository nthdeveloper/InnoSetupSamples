using System;
using System.Collections;
using System.Globalization;
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
        public const string SERVICE_NAME = "Sample Windows Service";

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
            serviceProcessInstaller.Account = ServiceAccount.User;

            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);

            InitializeComponent();
        }

        public string GetContextParameter(string key)
        {
            string sValue = "";
            try
            {
                sValue = this.Context.Parameters[key].ToString(CultureInfo.InvariantCulture);
            }
            catch
            {
                sValue = "";
            }
            return sValue;
        }

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);

            string username = GetContextParameter("username").Trim();
            string password = GetContextParameter("password").Trim();

            if (username != "")
                serviceProcessInstaller.Username = username;
            if (password != "")
                serviceProcessInstaller.Password = password;
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);

            ServiceController _serviceController = new ServiceController(SERVICE_NAME);
            try
            {
                _serviceController.Start();
            }
            catch
            {
            }
            finally
            {
                _serviceController.Dispose();
            }
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
