using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    interface ISimApp : INotifyPropertyChanged
    {
        int Indicated_heading_deg { get; set; }
        int Gps_indicated_vertical_speed { get; set; }
        int Gps_indicated_ground_speed_kt { get; set; }
        int Airspeed_indicator_indicated_speed_kt { get; set; }
        int Gps_indicated_altitude_ft { get; set; }
        int Attitude_indicator_internal_roll_deg { get; set; }
        int Attitude_indicator_internal_pitch_deg { get; set; }
        int Altimeter_indicated_altitude_ft { get; set; }
        void FlyPlane(int elevator, int rudder);
        void moveThrottle(int throttle);
        void moveAileron(int aileron);
        void connect(string ip, int port);
        void disconnect();
        void start();

    }
}
