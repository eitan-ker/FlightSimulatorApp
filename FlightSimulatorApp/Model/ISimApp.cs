using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    public interface ISimApp : INotifyPropertyChanged
    {
        string Indicated_heading_deg { get; set; }
        string Gps_indicated_vertical_speed { get; set; }
        string Gps_indicated_ground_speed_kt { get; set; }
        string Airspeed_indicator_indicated_speed_kt { get; set; }
        string Gps_indicated_altitude_ft { get; set; }
        string Attitude_indicator_internal_roll_deg { get; set; }
        string Attitude_indicator_internal_pitch_deg { get; set; }
        string Altimeter_indicated_altitude_ft { get; set; }
        string Latitude_deg { get; set; }
        string Longitude_deg { get; set; }
        string Locations { get; set; }
        void FlyPlane(double elevator, double rudder);
        void moveThrottle(double throttle);
        void moveAileron(string aileron);
        void connect(string ip, int port);
        void disconnect();
        void start();
        string ConnectionStatus { get; set; }
    }
}
