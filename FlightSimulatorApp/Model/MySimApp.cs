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
        public Dictionary<string, object> CodeMapsend;
        public Dictionary<string, object> CodeMaprecieve;
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
        private double longitude_deg; //logtitude of the plane


        ItelnetClient _telnetClient;
        volatile Boolean stop;

        public MySimApp(ItelnetClient telnetClient)
        {
            this._telnetClient = telnetClient;
            stop = false;
            CodeMapsend = new Dictionary<string, object>();
            CodeMapsend.Add("get /instrumentation/heading-indicator/indicated-heading-deg", this.Indicated_heading_deg);
            CodeMapsend.Add("get /instrumentation/gps/indicated-vertical-speed", this.Gps_indicated_vertical_speed);
            CodeMapsend.Add("get /instrumentation/gps/indicated-ground-speed-kt", this.Gps_indicated_ground_speed_kt);
            CodeMapsend.Add("get /instrumentation/airspeed-indicator/indicated-speed-kt", this.Airspeed_indicator_indicated_speed_kt);
            CodeMapsend.Add("get /instrumentation/altimeter/indicated-altitude-ft", this.Gps_indicated_altitude_ft);
            CodeMapsend.Add("get /instrumentation/attitude-indicator/internal-roll-deg", this.Attitude_indicator_internal_roll_deg);
            CodeMapsend.Add("get /instrumentation/attitude-indicator/internal-pitch-deg", this.Attitude_indicator_internal_pitch_deg);
            CodeMapsend.Add("get /instrumentation/gps/indicated-altitude-ft", this.Altimeter_indicated_altitude_ft);
            CodeMapsend.Add("get /position/latitude-deg", this.Latitude_deg);
            CodeMapsend.Add("get /position/longitude-deg", this.Longitude_deg);

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public double Latitude_deg
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /position/latitude-deg"))
                {
                    if (Double.TryParse(CodeMapsend["get /position/latitude-deg"].ToString(), out this.latitude_deg))
                    {
                        return this.latitude_deg;
                    }
                    else
                    {
                        throw new Exception("Latitude_deg has a non-numeric value");
                    }
                }
                else
                {
                    return this.latitude_deg;
                }
            }
            set
            {
                CodeMapsend["get /position/latitude-deg"] = value;
                NotifyPropertyChanged("Latitude_deg");
            }
        }
        public double Longitude_deg
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /position/longitude-deg"))
                {
                    if (Double.TryParse(CodeMapsend["get /position/longitude-deg"].ToString(), out longitude_deg))
                    {
                        return this.longitude_deg;
                    }
                    else
                    {
                        throw new Exception("Longtitue_deg has a non-numeric value");
                    }
                }
                else
                {
                    return this.longitude_deg;
                }
            }
            set
            {
                CodeMapsend["get /position/longitude-deg"] = value;
                NotifyPropertyChanged("Longitude_deg");
            }
        }
        public int Indicated_heading_deg
        {

            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/heading-indicator/indicated-heading-deg"))
                {
                    if (int.TryParse(CodeMapsend["get /instrumentation/heading-indicator/indicated-heading-deg"].ToString(), out indicated_heading_deg))
                    {
                        return this.indicated_heading_deg;
                    }
                    else
                    {
                        throw new Exception("Indicated_heading_deg has a non-numeric value");
                    }
                }
                else
                {
                    return this.indicated_heading_deg;
                }
            }
            set
            {
                CodeMapsend["get /instrumentation/heading-indicator/indicated-heading-deg"] = value;
                NotifyPropertyChanged("Indicated_heading_deg");
            }
        }
        public int Gps_indicated_vertical_speed
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/gps/indicated-vertical-speed"))
                {
                    if (int.TryParse(CodeMapsend["get /instrumentation/gps/indicated-vertical-speed"].ToString(), out this.gps_indicated_vertical_speed))
                    {
                        return this.gps_indicated_vertical_speed;
                    }
                    else
                    {
                        throw new Exception("Gps_indicated_vertical_speed has a non-numeric value");
                    }
                }
                else
                {
                    return this.gps_indicated_vertical_speed;
                }
            }
            set
            {
                CodeMapsend["get /instrumentation/gps/indicated-vertical-speed"] = value;
                NotifyPropertyChanged("Gps_indicated_vertical_speed");
            }
        }
        public int Gps_indicated_ground_speed_kt
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/gps/indicated-ground-speed-kt"))
                {
                    if (int.TryParse(CodeMapsend["get /instrumentation/gps/indicated-ground-speed-kt"].ToString(), out this.gps_indicated_ground_speed_kt))
                    {
                        return this.gps_indicated_ground_speed_kt;
                    }
                    else
                    {
                        throw new Exception("Gps_indicated_ground_speed_kt has a non-numeric value");
                    }
                }
                else
                {
                    return this.gps_indicated_ground_speed_kt;
                }
            }
            set
            {
                CodeMapsend["get /instrumentation/gps/indicated-ground-speed-kt"] = value;
                NotifyPropertyChanged("Gps_indicated_ground_speed_kt");
            }
        }
        public int Airspeed_indicator_indicated_speed_kt
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/airspeed-indicator/indicated-speed-kt"))
                {
                    if (int.TryParse(CodeMapsend["get /instrumentation/airspeed-indicator/indicated-speed-kt"].ToString(), out this.airspeed_indicator_indicated_speed_kt))
                    {
                        return this.airspeed_indicator_indicated_speed_kt;
                    }
                    else
                    {
                        throw new Exception("Airspeed_indicator_indicated_speed_kt has a non-numeric value");
                    }
                }
                else
                {
                    return this.airspeed_indicator_indicated_speed_kt;
                }
            }
            set
            {
                CodeMapsend["get /instrumentation/airspeed-indicator/indicated-speed-kt"] = value;
                NotifyPropertyChanged("Airspeed_indicator_indicated_speed_kt");
            }
        }
        public int Gps_indicated_altitude_ft
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/altimeter/indicated-altitude-ft"))
                {
                    if (int.TryParse(CodeMapsend["get /instrumentation/altimeter/indicated-altitude-ft"].ToString(), out this.gps_indicated_altitude_ft))
                    {
                        return this.airspeed_indicator_indicated_speed_kt;
                    }
                    else
                    {
                        throw new Exception("Airspeed_indicator_indicated_speed_kt has a non-numeric value");
                    }
                }
                else
                {
                    return this.gps_indicated_altitude_ft;
                }
            }
            set
            {
                CodeMapsend["get /instrumentation/altimeter/indicated-altitude-ft"] = value;
                NotifyPropertyChanged("Gps_indicated_altitude_ft");
            }
        }
        public int Attitude_indicator_internal_roll_deg
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/attitude-indicator/internal-roll-deg"))
                {
                    if (int.TryParse(CodeMapsend["get /instrumentation/attitude-indicator/internal-roll-deg"].ToString(), out this.attitude_indicator_internal_roll_deg))
                    {
                        return this.attitude_indicator_internal_roll_deg;
                    }
                    else
                    {
                        throw new Exception("Airspeed_indicator_indicated_speed_kt has a non-numeric value");
                    }
                }
                else
                {
                    return this.attitude_indicator_internal_roll_deg;
                }
            }
            set
            {
                CodeMapsend["get /instrumentation/attitude-indicator/internal-roll-deg"] = value;
                NotifyPropertyChanged("Attitude_indicator_internal_roll_deg");
            }
        }
        public int Attitude_indicator_internal_pitch_deg
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/attitude-indicator/internal-pitch-deg"))
                {
                    if (int.TryParse(CodeMapsend["get /instrumentation/attitude-indicator/internal-pitch-deg"].ToString(), out this.attitude_indicator_internal_pitch_deg))
                    {
                        return this.attitude_indicator_internal_pitch_deg;
                    }
                    else
                    {
                        throw new Exception("Attitude_indicator_internal_pitch_deg has a non-numeric value");
                    }
                }
                else
                {
                    return this.attitude_indicator_internal_pitch_deg;
                }
            }
            set
            {
                CodeMapsend["get /instrumentation/attitude-indicator/internal-pitch-deg"] = value;
                NotifyPropertyChanged("Attitude_indicator_internal_pitch_deg");
            }
        }
        public int Altimeter_indicated_altitude_ft
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/gps/indicated-altitude-ft"))
                {
                    if (int.TryParse(CodeMapsend["get /instrumentation/gps/indicated-altitude-ft"].ToString(), out this.altimeter_indicated_altitude_ft))
                    {
                        return this.altimeter_indicated_altitude_ft;
                    }
                    else
                    {
                        throw new Exception("Airspeed_indicator_indicated_speed_kt has a non-numeric value");
                    }
                }
                return this.altimeter_indicated_altitude_ft;
            }
            set
            {
                CodeMapsend["get /instrumentation/gps/indicated-altitude-ft"] = value;
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
            sb = new StringBuilder("set " + this.var_locations_in_simulator_send[1] + " " + rudder + "/n"); //build the command to set the rudder value in sim
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
