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
        void FlyPlane(int elevator, int rudder);
        void moveThrottle(int throttle);
        void moveAileron(int aileron);
        void connect(string ip, int port);
        void disconnect();
        void start();

    }
}
