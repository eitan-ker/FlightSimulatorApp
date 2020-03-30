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

        
        public event PropertyChangedEventHandler PropertyChanged;
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
        /********************************** related to Main Window */
        public string VM_ConnectionStatus => model.ConnectionStatus;
        internal void disconnect()
        {
            model.disconnect();
        }

        /********************************* related to dashboard*/
        public ViewModelClass(ISimApp simApp)
        {
            model = simApp;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public string VM_Indicated_heading_deg
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
        private double VM_aileron;
        public double VM_Aileron
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
        private double VM_throttle;
        public double VM_Throttle
        {
            get
            {
                return VM_throttle;
            }
            set
            {
                VM_throttle = value;
                model.moveThrottle(VM_throttle);
            }
        }
        public void FlyPlane(double elevator, double aileron)
        {
            model.FlyPlane(elevator, aileron);
        }
        
        /*****************************************/

        /************************ this belongs to map*/
        /*private double longtitude;
        public double VM_Longtitude_deg
        {
            get
            {
                return model.Longitude_deg;
            }
            set
            {
                location.Longitude = value;
            }
        }
        public double VM_Latitude_deg
        {
            get
            {
                return model.Latitude_deg;
            }
            set
            {
                
                location.Latitude = value;
            }
        }
        private Location location;*/
        public string VM_Locations
        {
            get
            {
               // Console.WriteLine(model.Locations);
                return model.Locations;
            }
            set
            {
                
            }
        }

    }
}
