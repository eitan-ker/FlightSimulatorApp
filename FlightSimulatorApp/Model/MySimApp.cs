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

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if(this.PropertyChanged!=null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public int Indicated_heading_deg {
            get {
                return this.indicated_heading_deg;
             }
            set {
                this.indicated_heading_deg = value;
                NotifyPropertyChanged("Indicated_heading_deg");
            }
        }
        public int Gps_indicated_vertical_speed
        {
            get
            {
                return this.gps_indicated_vertical_speed;
            }
            set
            {
                this.gps_indicated_vertical_speed = value;
                NotifyPropertyChanged("Gps_indicated_vertical_speed");
            }
        }
        public int Gps_indicated_ground_speed_kt
        {
            get
            {
                return this.gps_indicated_ground_speed_kt;
            }
            set
            {
                this.gps_indicated_ground_speed_kt = value;
                NotifyPropertyChanged("Gps_indicated_ground_speed_kt");
            }
        }
        public int Airspeed_indicator_indicated_speed_kt
        {
            get
            {
                return this.airspeed_indicator_indicated_speed_kt;
            }
            set
            {
                this.airspeed_indicator_indicated_speed_kt = value;
                NotifyPropertyChanged("Airspeed_indicator_indicated_speed_kt");
            }
        }
        public int Gps_indicated_altitude_ft
        {
            get
            {
                return this.gps_indicated_altitude_ft;
            }
            set
            {
                this.gps_indicated_altitude_ft = value;
                NotifyPropertyChanged("Gps_indicated_altitude_ft");
            }
        }
        public int Attitude_indicator_internal_roll_deg
        {
            get
            {
                return this.attitude_indicator_internal_roll_deg;
            }
            set
            {
                this.attitude_indicator_internal_roll_deg = value;
                NotifyPropertyChanged("Attitude_indicator_internal_roll_deg");
            }
        }
        public int Attitude_indicator_internal_pitch_deg
        {
            get
            {
                return this.attitude_indicator_internal_pitch_deg;
            }
            set
            {
                this.attitude_indicator_internal_pitch_deg = value;
                NotifyPropertyChanged("Attitude_indicator_internal_pitch_deg");
            }
        }
        public int Altimeter_indicated_altitude_ft
        {
            get
            {
                return this.altimeter_indicated_altitude_ft;
            }
            set
            {
                this.altimeter_indicated_altitude_ft = value;
                NotifyPropertyChanged("Altimeter_indicated_altitude_ft");
            }
        }

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

        public void moveAileron(int aileron)
        {
            throw new NotImplementedException();
        }

        public void moveThrottle(int throttle)
        {
            throw new NotImplementedException();
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
