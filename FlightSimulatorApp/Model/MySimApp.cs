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
        private double indicated_heading_deg;
        private double gps_indicated_vertical_speed;
        private double gps_indicated_ground_speed_kt;
        private double airspeed_indicator_indicated_speed_kt;
        private double gps_indicated_altitude_ft; //ALTIMETER = '/instrumentation/altimeter/indicated-altitude-ft'?
        private double attitude_indicator_internal_roll_deg;
        private double attitude_indicator_internal_pitch_deg;
        private double altimeter_indicated_altitude_ft;
        private double latitude_deg; //latitude of the plane
        private double longitude_deg; //logtitude of the plane


        ItelnetClient _telnetClient;
        volatile Boolean stop;

        public MySimApp(ItelnetClient telnetClient)
        {
            this._telnetClient = telnetClient;
            stop = false;
            CodeMapsend = new Dictionary<string, object>();
            CodeMapsend.Add("get /instrumentation/heading-indicator/indicated-heading-deg\n", this.Indicated_heading_deg);
            CodeMapsend.Add("get /instrumentation/gps/indicated-vertical-speed\n", this.Gps_indicated_vertical_speed);
            CodeMapsend.Add("get /instrumentation/gps/indicated-ground-speed-kt\n", this.Gps_indicated_ground_speed_kt);
            CodeMapsend.Add("get /instrumentation/airspeed-indicator/indicated-speed-kt\n", this.Airspeed_indicator_indicated_speed_kt);
            CodeMapsend.Add("get /instrumentation/altimeter/indicated-altitude-ft\n", this.Gps_indicated_altitude_ft);
            CodeMapsend.Add("get /instrumentation/attitude-indicator/internal-roll-deg\n", this.Attitude_indicator_internal_roll_deg);
            CodeMapsend.Add("get /instrumentation/attitude-indicator/internal-pitch-deg\n", this.Attitude_indicator_internal_pitch_deg);
            CodeMapsend.Add("get /instrumentation/gps/indicated-altitude-ft\n", this.Altimeter_indicated_altitude_ft);
            CodeMapsend.Add("get /position/latitude-deg\n", this.Latitude_deg);
            CodeMapsend.Add("get /position/longitude-deg\n", this.Longitude_deg);

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
                if (CodeMapsend.ContainsKey("get /position/latitude-deg\n"))
                {
                    if (Double.TryParse(CodeMapsend["get /position/latitude-deg\n"].ToString(), out this.latitude_deg))
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
                this.latitude_deg = value;
                //CodeMapsend["get /position/latitude-deg\n"] = value;
                NotifyPropertyChanged("Latitude_deg");
            }
        }
        public double Longitude_deg
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /position/longitude-deg\n"))
                {
                    if (Double.TryParse(CodeMapsend["get /position/longitude-deg\n"].ToString(), out longitude_deg))
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
                this.longitude_deg = value;
                //CodeMapsend["get /position/longitude-deg\n"] = value;
                NotifyPropertyChanged("Longitude_deg");
            }
        }
        public double Indicated_heading_deg
        {

            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/heading-indicator/indicated-heading-deg\n"))
                {
                    if (double.TryParse(CodeMapsend["get /instrumentation/heading-indicator/indicated-heading-deg\n"].ToString(), out indicated_heading_deg))
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
                this.indicated_heading_deg = value;
               // CodeMapsend["get /instrumentation/heading-indicator/indicated-heading-deg\n"] = value;
                NotifyPropertyChanged("Indicated_heading_deg");
            }
        }
        public double Gps_indicated_vertical_speed
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/gps/indicated-vertical-speed\n"))
                {
                    if (double.TryParse(CodeMapsend["get /instrumentation/gps/indicated-vertical-speed\n"].ToString(), out this.gps_indicated_vertical_speed))
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
                this.gps_indicated_vertical_speed = value;
                //CodeMapsend["get /instrumentation/gps/indicated-vertical-speed\n"] = value;
                NotifyPropertyChanged("Gps_indicated_vertical_speed");
            }
        }
        public double Gps_indicated_ground_speed_kt
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/gps/indicated-ground-speed-kt\n"))
                {
                    if (double.TryParse(CodeMapsend["get /instrumentation/gps/indicated-ground-speed-kt\n"].ToString(), out this.gps_indicated_ground_speed_kt))
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
                this.gps_indicated_ground_speed_kt = value;
                //CodeMapsend["get /instrumentation/gps/indicated-ground-speed-kt\n"] = value;
                NotifyPropertyChanged("Gps_indicated_ground_speed_kt");
            }
        }
        public double Airspeed_indicator_indicated_speed_kt
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/airspeed-indicator/indicated-speed-kt\n"))
                {
                    if (double.TryParse(CodeMapsend["get /instrumentation/airspeed-indicator/indicated-speed-kt\n"].ToString(), out this.airspeed_indicator_indicated_speed_kt))
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
                this.airspeed_indicator_indicated_speed_kt = value;
                //CodeMapsend["get /instrumentation/airspeed-indicator/indicated-speed-kt\n"] = value;
                NotifyPropertyChanged("Airspeed_indicator_indicated_speed_kt");
            }
        }
        public double Gps_indicated_altitude_ft
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/altimeter/indicated-altitude-ft\n"))
                {
                    if (double.TryParse(CodeMapsend["get /instrumentation/altimeter/indicated-altitude-ft\n"].ToString(), out this.gps_indicated_altitude_ft))
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
                this.gps_indicated_altitude_ft = value;
                //CodeMapsend["get /instrumentation/altimeter/indicated-altitude-ft\n"] = value;
                NotifyPropertyChanged("Gps_indicated_altitude_ft");
            }
        }
        public double Attitude_indicator_internal_roll_deg
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/attitude-indicator/internal-roll-deg\n"))
                {
                    if (double.TryParse(CodeMapsend["get /instrumentation/attitude-indicator/internal-roll-deg\n"].ToString(), out this.attitude_indicator_internal_roll_deg))
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
                this.attitude_indicator_internal_roll_deg = value;
                //CodeMapsend["get /instrumentation/attitude-indicator/internal-roll-deg\n"] = value;
                NotifyPropertyChanged("Attitude_indicator_internal_roll_deg");
            }
        }
        public double Attitude_indicator_internal_pitch_deg
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/attitude-indicator/internal-pitch-deg\n"))
                {
                    if (double.TryParse(CodeMapsend["get /instrumentation/attitude-indicator/internal-pitch-deg\n"].ToString(), out this.attitude_indicator_internal_pitch_deg))
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
                this.attitude_indicator_internal_pitch_deg = value;
               // CodeMapsend["get /instrumentation/attitude-indicator/internal-pitch-deg\n"] = value;
                NotifyPropertyChanged("Attitude_indicator_internal_pitch_deg");
            }
        }
        public double Altimeter_indicated_altitude_ft
        {
            get
            {
                if (CodeMapsend.ContainsKey("get /instrumentation/gps/indicated-altitude-ft\n"))
                {
                    if (double.TryParse(CodeMapsend["get /instrumentation/gps/indicated-altitude-ft\n"].ToString(), out this.altimeter_indicated_altitude_ft))
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
                this.altimeter_indicated_altitude_ft = value;
              //  CodeMapsend["get /instrumentation/gps/indicated-altitude-ft\n"] = value;
                NotifyPropertyChanged("Altimeter_indicated_altitude_ft");
            }
        }

        public void connect(string ip, int port)
        {
            _telnetClient.connect(ip, port);
            this.start();
        }
        public void disconnect()
        {
            stop = true;
            _telnetClient.disconnect();
        }
        public void FlyPlane(int elevator, int rudder)
        {
            StringBuilder sb = new StringBuilder("set " + this.var_locations_in_simulator_send[2] + " " + elevator + "\n"); //build the command to set the elevator value in sim
            string elevatorCommand = sb.ToString();
            this._telnetClient.write(elevatorCommand);
            sb = new StringBuilder("set " + this.var_locations_in_simulator_send[1] + " " + rudder + "\n"); //build the command to set the rudder value in sim
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
                    Dictionary<string, object> temp = new Dictionary<string, object>(CodeMapsend);
                    foreach (KeyValuePair<string, object> entry in CodeMapsend)
                    {
                        _telnetClient.write(entry.Key);

                        temp[entry.Key] = Double.Parse(_telnetClient.read());
                        
                    }
                    CodeMapsend = temp;
                    Thread.Sleep(250); // read data in 4Hz
                }
            }).Start();
        }
    }
}
