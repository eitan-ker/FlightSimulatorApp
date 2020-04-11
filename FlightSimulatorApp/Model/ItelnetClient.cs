using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    public interface ItelnetClient
    {
        void connect(string ip, int port);
        void write(string command);
        string read(string value);
        void disconnect();
        bool checkConnectionStatus();
    }
}
