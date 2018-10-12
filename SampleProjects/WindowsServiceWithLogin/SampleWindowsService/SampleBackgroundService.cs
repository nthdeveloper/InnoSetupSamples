using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleWindowsService
{
    class SampleBackgroundService
    {
        bool stopRequested;

        public bool Start()
        {
            Thread thr = new Thread(serviceThread);
            thr.Start();

            return true;
        }

        public void Stop()
        {
            stopRequested = true;
        }

        private void serviceThread()
        {
            while(!stopRequested)
            {
                Thread.Sleep(1000);

                Console.WriteLine("I'm running...");
            }
        }
    }
}
