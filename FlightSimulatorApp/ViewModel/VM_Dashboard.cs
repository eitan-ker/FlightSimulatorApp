using FlightSimulatorApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    class VM_Dashboard : INotifyPropertyChanged
    {
        private ISimApp model;

        public event PropertyChangedEventHandler PropertyChanged;
        public VM_Dashboard(ISimApp simApp)
        {
            model = simApp;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public void NotifyPropertyChanged(string propName)
        {
            /*var handler = PropertyChanged;
            if(handler!=null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }*/

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public string VM_Indicated_heading_deg => model.Indicated_heading_deg;
        public double VM_Gps_indicated_vertical_speed => model.Gps_indicated_vertical_speed;
        public double VM_Gps_indicated_ground_speed_kt => model.Gps_indicated_ground_speed_kt;
        public double VM_Airspeed_indicator_indicated_speed_kt => model.Airspeed_indicator_indicated_speed_kt;


        public double VM_Gps_indicated_altitude_ft => model.Gps_indicated_altitude_ft;

        public double VM_Attitude_indicator_internal_roll_deg => model.Attitude_indicator_internal_roll_deg;

        public double VM_Attitude_indicator_internal_pitch_deg => model.Attitude_indicator_internal_pitch_deg;

        public double VM_Altimeter_indicated_altitude_ft => model.Altimeter_indicated_altitude_ft;
    }
}
