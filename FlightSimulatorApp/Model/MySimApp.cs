using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    class MySimApp : ISimApp
    {
        ItelnetClient _telnetClient;
        volatile Boolean stop;

        public MySimApp(ItelnetClient telnetClient)
        {
            this._telnetClient = telnetClient;
            stop = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void connect(string ip, int port)
        {
            _telnetClient.connect(ip, port);
        }
        public void disconnect()
        {
            stop = true;
            _telnetClient.disconnect();
        }
        public void start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {

                }
            }).Start();
        }
    }
}
