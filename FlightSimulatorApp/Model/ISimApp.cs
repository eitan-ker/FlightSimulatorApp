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
        double Indicated_heading_deg { get; set; }
        double Gps_indicated_vertical_speed { get; set; }
        double Gps_indicated_ground_speed_kt { get; set; }
        double Airspeed_indicator_indicated_speed_kt { get; set; }
        double Gps_indicated_altitude_ft { get; set; }
        double Attitude_indicator_internal_roll_deg { get; set; }
        double Attitude_indicator_internal_pitch_deg { get; set; }
        double Altimeter_indicated_altitude_ft { get; set; }
        double Latitude_deg { get; set; }
        double Longitude_deg { get; set; }
        string Locations { get; set; }
        void FlyPlane(double elevator, double rudder);
        void moveThrottle(double throttle);
        void moveAileron(double aileron);
        void connect(string ip, int port);
        void disconnect();
        void start();
        string ConnectionStatus { get; set; }
    }
}
