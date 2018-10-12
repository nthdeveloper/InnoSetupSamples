using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SampleWindowsService
{
    public partial class SampleWindowsService : ServiceBase
    {
        SampleBackgroundService sampleBackgroundService;

        public SampleWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            sampleBackgroundService = new SampleBackgroundService();

            if(!sampleBackgroundService.Start())
                throw new Exception("Services could not be started.");
        }

        protected override void OnStop()
        {
            sampleBackgroundService.Stop();
        }
    }
}
