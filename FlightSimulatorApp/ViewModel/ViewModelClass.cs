using FlightSimulatorApp.Model;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModel
{
    public class ViewModelClass : INotifyPropertyChanged
    {
        public ISimApp model;

        /********************************* related to dashboard*/
        public event PropertyChangedEventHandler PropertyChanged;
        public ViewModelClass(ISimApp simApp)
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public double VM_Indicated_heading_deg
        {
            get
            {
                return model.Indicated_heading_deg;
            }
            set
            {

            }
        }
        public double VM_Gps_indicated_vertical_speed => model.Gps_indicated_vertical_speed;
        public double VM_Gps_indicated_ground_speed_kt => model.Gps_indicated_ground_speed_kt;
        public double VM_Airspeed_indicator_indicated_speed_kt => model.Airspeed_indicator_indicated_speed_kt;

        public double VM_Gps_indicated_altitude_ft => model.Gps_indicated_altitude_ft;

        public double VM_Attitude_indicator_internal_roll_deg => model.Attitude_indicator_internal_roll_deg;

        public double VM_Attitude_indicator_internal_pitch_deg => model.Attitude_indicator_internal_pitch_deg;

        public double VM_Altimeter_indicated_altitude_ft => model.Altimeter_indicated_altitude_ft;
        /**************************************************/


        /*********************************************this belongs to Joystick*/
        private int VM_aileron;
        public int VM_Aileron
        {
            get
            {
                return VM_aileron;
            }
            set
            {
                VM_aileron = value;
                model.moveAileron(VM_aileron);
            }
        }
        private int VM_throttle;
        public int VM_Throttle
        {
            get
            {
                return VM_throttle;
            }
            set
            {
                VM_throttle = value;
                model.moveAileron(throttle);
            }
        }
        private int VM_elevator;
        public int VM_Elevator
        {
            get
            {
                return VM_elevator;
            }
            set
            {

            }
        }
        private int VM_rudder;
        public int VM_Rudder
        {
            get
            {
                return VM_rudder;
            }
            set
            {

            }
        }
        /*****************************************/

        /************************ this belongs to map*/
        private double longtitude;
        public double Longtitude
        {
            get
            {
                return longtitude;
            }
            set
            {
                longtitude = value;
                location.Longitude = value;
            }
        }
        private double latitude;
        public double Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                latitude = value;
                location.Latitude = value;
            }
        }
        private Location location;
        public Location VM_Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                NotifyPropertyChanged("Location");
            }
        }

    }
}
