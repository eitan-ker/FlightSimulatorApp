using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    class MyMapViewModel : INotifyPropertyChanged
    {
        private ImapModel mapModel;
        public event PropertyChangedEventHandler PropertyChanged;
        MyMapViewModel(ImapModel mapmodel)
        {
            this.mapModel = mapmodel;
        }
    }
}
