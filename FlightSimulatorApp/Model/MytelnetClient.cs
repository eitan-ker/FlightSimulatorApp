using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    public sealed class MytelnetClient : ItelnetClient
    {
        private static readonly object padlock = new object();
        private static MytelnetClient instance = null;
        public static MytelnetClient Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new MytelnetClient();
                        }
                    }
                }
                return instance;
            }
        }

        public void connect(string ip, int port)
        {
            throw new NotImplementedException();
        }

        public void disconnect()
        {
            throw new NotImplementedException();
        }

        public string read()
        {
            throw new NotImplementedException();
        }

        public void write(string command)
        {
            throw new NotImplementedException();
        }
    }
}