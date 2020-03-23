using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    interface ISimApp : INotifyPropertyChanged
    {
        void connect(string ip, int port);
        void disconnect();
        void start();
    }
}
