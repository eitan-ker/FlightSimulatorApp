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
        public Dictionary<string, object> CodeMap;
        string[] var_locations_in_simulator_send = {"/controls/engines/current-engine/throttle", "/controls/flight/rudder", "/controls/flight/elevator",
        "/controls/flight/aileron"};
        string[] var_locations_in_simulator_recieve = {"/instrumentation/heading-indicator/indicated-heading-deg", "/instrumentation/gps/indicated-vertical-speed",
            "/instrumentation/gps/indicated-ground-speed-kt", "/instrumentation/airspeed-indicator/indicated-speed-kt", "/instrumentation/attitude-indicator/internal-roll-deg",
            "/instrumentation/attitude-indicator/internal-pitch-deg", "/instrumentation/gps/indicated-altitude-ft", "/position/latitude-deg", "/position/longitude-deg"};
        private int indicated_heading_deg;
        private int gps_indicated_vertical_speed;
        private int gps_indicated_ground_speed_kt;
        private int airspeed_indicator_indicated_speed_kt;
        private int gps_indicated_altitude_ft; //ALTIMETER = '/instrumentation/altimeter/indicated-altitude-ft'?
        private int attitude_indicator_internal_roll_deg;
        private int attitude_indicator_internal_pitch_deg;
        private int altimeter_indicated_altitude_ft;
        private double latitude_deg; //latitude of the plane
        private double longitude_deg //logtitude of the plane


        ItelnetClient _telnetClient;
        volatile Boolean stop;

        public MySimApp(ItelnetClient telnetClient)
        {
            this._telnetClient = telnetClient;
            stop = false;
            CodeMap = new Dictionary<string, object>();
            CodeMap.Add("/instrumentation/heading-indicator/indicated-heading-deg", this.Indicated_heading_deg);
            CodeMap.Add("/instrumentation/gps/indicated-vertical-speed", this.Gps_indicated_vertical_speed);
            CodeMap.Add("/instrumentation/gps/indicated-ground-speed-kt", this.Gps_indicated_ground_speed_kt);
            CodeMap.Add("/instrumentation/airspeed-indicator/indicated-speed-kt", this.Airspeed_indicator_indicated_speed_kt);
            CodeMap.Add("/instrumentation/altimeter/indicated-altitude-ft", this.Gps_indicated_altitude_ft);
            CodeMap.Add("/instrumentation/attitude-indicator/internal-roll-deg", this.Attitude_indicator_internal_roll_deg);
            CodeMap.Add("/instrumentation/attitude-indicator/internal-pitch-deg", this.Attitude_indicator_internal_pitch_deg);
            CodeMap.Add("/instrumentation/gps/indicated-altitude-ft", this.Altimeter_indicated_altitude_ft);
            CodeMap.Add("/position/latitude-deg", this.Gps_indicated_vertical_speed);
            CodeMap.Add("/position/longitude-deg", this.Gps_indicated_vertical_speed);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if(this.PropertyChanged!=null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public double Latitude_deg
        {
            get
            {
                if (Double.TryParse(CodeMap["/position/latitude-deg"].ToString(), out latitude_deg))
                {
                    return this.latitude_deg;
                }
                else
                {
                    throw new Exception("Indicated_heading_deg has a non-numeric value");
                }
            }
            set
            {
                CodeMap["/position/latitude-deg"] = value;
                NotifyPropertyChanged("Latitude_deg");
            }
        }
        public int Indicated_heading_deg {

            get {
                if (int.TryParse(CodeMap["stat"].ToString(), out indicated_heading_deg))
                {
                    return this.indicated_heading_deg;
                }
                else
                {
                    throw new Exception("Indicated_heading_deg has a non-numeric value");
                }
             }
            set {
                CodeMap["/instrumentation/heading-indicator/indicated-heading-deg"] = value;
                NotifyPropertyChanged("Indicated_heading_deg");
            }
        }
        public int Gps_indicated_vertical_speed
        {
            get
            {
                if (int.TryParse(CodeMap["/instrumentation/gps/indicated-vertical-speed"].ToString(), out this.gps_indicated_vertical_speed))
                {
                    return this.gps_indicated_vertical_speed;
                }
                else
                {
                    throw new Exception("Indicated_heading_deg has a non-numeric value");
                }
            }
            set
            {
                CodeMap["/instrumentation/gps/indicated-vertical-speed"] = value;
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
            StringBuilder sb = new StringBuilder("set " + this.var_locations_in_simulator_send[2] + " " + elevator + "/n"); //build the command to set the elevator value in sim
            string elevatorCommand = sb.ToString();
            this._telnetClient.write(elevatorCommand);
            sb = new StringBuilder("set " + this.var_locations_in_simulator_send[1] +" " + rudder + "/n"); //build the command to set the rudder value in sim
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
