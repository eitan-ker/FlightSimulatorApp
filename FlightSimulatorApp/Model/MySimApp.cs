using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    class MySimApp : ISimApp
    {
        private int indicated_heading_deg;
        private int gps_indicated_vertical_speed;
        private int gps_indicated_ground_speed_kt;
        private int airspeed_indicator_indicated_speed_kt;
        private int gps_indicated_altitude_ft;
        private int attitude_indicator_internal_roll_deg;
        private int attitude_indicator_internal_pitch_deg;
        private int altimeter_indicated_altitude_ft;


        ItelnetClient _telnetClient;
        volatile Boolean stop;

        public MySimApp(ItelnetClient telnetClient)
        {
            this._telnetClient = telnetClient;
            stop = false;
        }

        public int Indicated_heading_deg {
            get {
                throw new NotImplementedException()
             }
            set {
                throw new NotImplementedException();
            }
        }
        public int Gps_indicated_vertical_speed
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public int Gps_indicated_ground_speed_kt
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public int Airspeed_indicator_indicated_speed_kt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Gps_indicated_altitude_ft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Attitude_indicator_internal_roll_deg { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Attitude_indicator_internal_pitch_deg { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Altimeter_indicated_altitude_ft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event PropertyChangedEventHandler PropertyChanged;

        public void connect(string ip, int port)
        {
            _telnetClient.connect(ip, port);
        }
        public void disconnect()
        {
            stop = true;
            _telnetClient.disconnect();
        }
        public void FlyPlane(int elevator, int rudder)
        {
            StringBuilder sb = new StringBuilder("set /controls/flight/elevator " + elevator + "/n"); //build the command to set the elevator value in sim
            string elevatorCommand = sb.ToString();
            this._telnetClient.write(elevatorCommand);
            sb = new StringBuilder("set /controls/flight/rudder " + rudder + "/n"); //build the command to set the rudder value in sim
            string rudderCommand = sb.ToString();
            this._telnetClient.write(rudderCommand);
        }

        public void start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {

                }
            }).Start();
        }
    }
}
