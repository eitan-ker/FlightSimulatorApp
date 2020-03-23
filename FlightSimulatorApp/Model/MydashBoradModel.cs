using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    class MydashBoradModel : IdashBoardModel
    {
        private ItelnetClient telnetClient;
        public event PropertyChangedEventHandler PropertyChanged;
        MydashBoradModel(ItelnetClient telnetclient)
        {
            this.telnetClient = telnetclient;
        }

        public void connect(string ip, int port)
        {
            throw new NotImplementedException();
        }

        public void disconnect()
        {
            throw new NotImplementedException();
        }

        public void start()
        {
            throw new NotImplementedException();
        }
    }
}
