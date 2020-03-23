using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    class MyJoystickViewModel : INotifyPropertyChanged
    {
        private IjoystickModel joystickModel;
        public event PropertyChangedEventHandler PropertyChanged;
        MyJoystickViewModel(IjoystickModel joystickmodel)
        {
            this.joystickModel = joystickmodel;
        }
    }
}
