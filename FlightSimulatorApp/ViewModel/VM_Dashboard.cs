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
        private int VM_indicated_heading_deg;
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertChanged(string propName)
        {
            /*var handler = PropertyChanged;
            if(handler!=null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }*/

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public int VM_Indicated_heading_deg
        {
            get
            {
                return VM_indicated_heading_deg;
            }
            set
            {

            }
        }
        private int VM_gps_indicated_vertical_speed;
        public int VM_Gps_indicated_vertical_speed
        {
            get
            {
                return VM_gps_indicated_vertical_speed;
            }
            set
            {

            }
        }
        private int VM_gps_indicated_ground_speed_kt;
        public int VM_Gps_indicated_ground_speed_kt
        {
            get
            {
                return VM_gps_indicated_ground_speed_kt;
            }
            set
            {

            }
        }
        private int VM_airspeed_indicator_indicated_speed_kt;
        public int VM_Airspeed_indicator_indicated_speed_kt
        {
            get
            {
                return VM_airspeed_indicator_indicated_speed_kt;
            }
            set
            {

            }
        }
        private int VM_gps_indicated_altitude_ft;
        public int VM_Gps_indicated_altitude_ft
        {
            get
            {
                return VM_gps_indicated_altitude_ft;
            }
            set
            {

            }
        }
        private int VM_attitude_indicator_internal_roll_deg;
        public int VM_Attitude_indicator_internal_roll_deg
        {
            get
            {
                return VM_attitude_indicator_internal_roll_deg;
            }
            set
            {

            }
        }
        private int VM_attitude_indicator_internal_pitch_deg;
        public int VM_Attitude_indicator_internal_pitch_deg
        {
            get
            {
                return VM_attitude_indicator_internal_pitch_deg;
            }
            set
            {

            }
        }
        private int VM_altimeter_indicated_altitude_ft;

        public int VM_Altimeter_indicated_altitude_ft
        {
            get
            {
                return VM_altimeter_indicated_altitude_ft;
            }
            set
            {

            }
        }
        public VM_Dashboard(ISimApp simApp)
        {
            model = simApp;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
             {
                 //NotifyPropertyChanged("VM_" + e.PropertyName);
             };
        }
    }
}
