using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    class MyDashboardViewModel : INotifyPropertyChanged
    {
        private IdashBoardModel dashboardModel;
        public event PropertyChangedEventHandler PropertyChanged;
        MyDashboardViewModel(IdashBoardModel dashboardmodel)
        {
            this.dashboardModel = dashboardmodel;
        }
    }
}
