using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace Helper.Utility_Classes
{
    class Uploader: IDisposable

    {
        private string source, destination;
        public Uploader()
        {
            Thread t = new Thread(new ThreadStart(workerThread));
            t.IsBackground = true;
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
        public Uploader(string source1, string destination1)
        {
            source = source1;
            destination = destination1;
        }
        public void Dispose()
        {
        }

        private void stopThread()
        {
        }
        private void workerThread()
        {
            
        }
    }
}
