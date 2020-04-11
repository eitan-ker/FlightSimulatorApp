using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    public class MySimApp : ISimApp
    {
        private Mutex m = new Mutex();
        public Dictionary<string, object> CodeMapsend;
        public Dictionary<string, object> CodeMaprecieve;
        public Dictionary<string, double> thresholdValuestoThrottleandAileron;
        public Dictionary<string, object> temp;
        string[] var_locations_in_simulator_send = {"set /controls/engines/current-engine/throttle", "set /controls/flight/rudder", "set /controls/flight/elevator",
        "set /controls/flight/aileron"};
        string[] var_locations_in_simulator_recieve = {"/instrumentation/heading-indicator/indicated-heading-deg", "/instrumentation/gps/indicated-vertical-speed",
            "/instrumentation/gps/indicated-ground-speed-kt", "/instrumentation/airspeed-indicator/indicated-speed-kt", "/instrumentation/attitude-indicator/internal-roll-deg",
            "/instrumentation/attitude-indicator/internal-pitch-deg", "/instrumentation/gps/indicated-altitude-ft", "/position/latitude-deg", "/position/longitude-deg"};
        private string indicated_heading_deg;
        private string gps_indicated_vertical_speed;
        private string gps_indicated_ground_speed_kt;
        private string airspeed_indicator_indicated_speed_kt;
        private string gps_indicated_altitude_ft; //ALTIMETER = '/instrumentation/altimeter/indicated-altitude-ft'?
        private string attitude_indicator_internal_roll_deg;
        private string attitude_indicator_internal_pitch_deg;
        private string altimeter_indicated_altitude_ft;
        private string latitude_deg; //latitude of the plane
        private string longitude_deg; //logtitude of the plane
        private string connectionStatus = "Disconnected";


        ItelnetClient _telnetClient;
        volatile Boolean stop;

        public MySimApp(ItelnetClient telnetClient)
        {
            this._telnetClient = telnetClient;
            stop = false;
            CodeMapsend = new Dictionary<string, object>();
            this.restorebackTo();
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
            thresholdValuestoThrottleandAileron = new Dictionary<string, double>();
            thresholdValuestoThrottleandAileron.Add("min_dashboard_val", this.min_dashboard_val);
            thresholdValuestoThrottleandAileron.Add("max_dashboard_val", this.max_dashboard_val);

            temp = new Dictionary<string, object>(CodeMapsend);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        private string locations;

        public string ConnectionStatus
        {
            get
            {
                return this.connectionStatus;
            }
            set
            {
                this.connectionStatus = value;
                NotifyPropertyChanged("ConnectionStatus");
            }
        }

        public string Locations
        {
            get
            {
                return this.locations;
            }
            set
            {
                this.locations = value;
                NotifyPropertyChanged("Locations");
            }
        }
        public string Latitude_deg
        {
            get
            {
                return this.latitude_deg;
            }
            set
            {
                this.latitude_deg = value;
                //CodeMapsend["get /position/latitude-deg\n"] = value;
                //Locations.Latitude = value;
                NotifyPropertyChanged("Latitude_deg");
            }
        }
        public string Longitude_deg
        {
            get
            {
                return this.longitude_deg;

            }
            set
            {
                this.longitude_deg = value;
                //CodeMapsend["get /position/longitude-deg\n"] = value;
                //Locations.Longitude = value;
                NotifyPropertyChanged("Longitude_deg");
            }
        }
        public string Indicated_heading_deg
        {

            get
            {
                return this.indicated_heading_deg;

            }
            set
            {
                this.indicated_heading_deg = value;
                //CodeMapsend["get /instrumentation/heading-indicator/indicated-heading-deg\n"] = value;
                NotifyPropertyChanged("Indicated_heading_deg");
            }
        }
        public string Gps_indicated_vertical_speed
        {
            get
            {

                return this.gps_indicated_vertical_speed;

            }
            set
            {
                this.gps_indicated_vertical_speed = value;
                //CodeMapsend["get /instrumentation/gps/indicated-vertical-speed\n"] = value;
                NotifyPropertyChanged("Gps_indicated_vertical_speed");
            }
        }
        public string Gps_indicated_ground_speed_kt
        {
            get
            {

                return this.gps_indicated_ground_speed_kt;

            }
            set
            {
                this.gps_indicated_ground_speed_kt = value;
                //CodeMapsend["get /instrumentation/gps/indicated-ground-speed-kt\n"] = value;
                NotifyPropertyChanged("Gps_indicated_ground_speed_kt");
            }
        }
        public string Airspeed_indicator_indicated_speed_kt
        {
            get
            {
                return this.airspeed_indicator_indicated_speed_kt;

            }
            set
            {
                this.airspeed_indicator_indicated_speed_kt = value;
                //CodeMapsend["get /instrumentation/airspeed-indicator/indicated-speed-kt\n"] = value;
                NotifyPropertyChanged("Airspeed_indicator_indicated_speed_kt");
            }
        }
        public string Gps_indicated_altitude_ft
        {
            get
            {
                return this.gps_indicated_altitude_ft;

            }
            set
            {
                this.gps_indicated_altitude_ft = value;
                //CodeMapsend["get /instrumentation/altimeter/indicated-altitude-ft\n"] = value;
                NotifyPropertyChanged("Gps_indicated_altitude_ft");
            }
        }
        public string Attitude_indicator_internal_roll_deg
        {
            get
            {
                return this.attitude_indicator_internal_roll_deg;

            }
            set
            {
                this.attitude_indicator_internal_roll_deg = value;
                //CodeMapsend["get /instrumentation/attitude-indicator/internal-roll-deg\n"] = value;
                NotifyPropertyChanged("Attitude_indicator_internal_roll_deg");
            }
        }
        public string Attitude_indicator_internal_pitch_deg
        {
            get
            {
                return this.attitude_indicator_internal_pitch_deg;

            }
            set
            {
                this.attitude_indicator_internal_pitch_deg = value;
                //CodeMapsend["get /instrumentation/attitude-indicator/internal-pitch-deg\n"] = value;
                NotifyPropertyChanged("Attitude_indicator_internal_pitch_deg");
            }
        }
        public string Altimeter_indicated_altitude_ft
        {
            get
            {
                return this.altimeter_indicated_altitude_ft;
            }
            set
            {
                this.altimeter_indicated_altitude_ft = value;
                //CodeMapsend["get /instrumentation/gps/indicated-altitude-ft\n"] = value;
                NotifyPropertyChanged("Altimeter_indicated_altitude_ft");
            }
        }

        /*************************************************** max and min values to check if the set command values are between the threshold*/
        /*private double min_Throttle = 0;
        private double max_Throttle = 1;
        private double min_Aileron = -1;
        private double max_Aileron = 1;*/
        private double min_dashboard_val = 1;
        private double max_dashboard_val = 8;
        public double Min_dashboard_val => this.min_dashboard_val;
        public double Max_dashboard_val => this.max_dashboard_val;


        public string checkThreshold_For_Dashboard_vars(string val)
        {
            /*thresholdValuestoThrottleandAileron[var_name] = val;
            double min_threshold = thresholdValuestoThrottleandAileron["min" + var_name];
            double max_threshold = thresholdValuestoThrottleandAileron["max" + var_name];
            string command;
            if (val > thresholdValuestoThrottleandAileron["min"+var_name])
            {
                if (val < thresholdValuestoThrottleandAileron["max" + var_name])
                {
                    command = "set " + var_name + " " + val;
                } else
                {
                    command = "set " + var_name + " " + thresholdValuestoThrottleandAileron["max" + var_name]; ;
                }
            } else
            {
                command = "set " + var_name + " " + thresholdValuestoThrottleandAileron["min" + var_name];
            }
            this._telnetClient.write(command); NEED TO CHECK IF ACCORDING TO REQUIREMENTS DOCUMENT*/

            string double_STR_To_Send;
            double STR_to_double = Double.Parse(val);
            if (STR_to_double > this.min_dashboard_val)
            {
                if (STR_to_double > this.Max_dashboard_val)
                {
                    double_STR_To_Send = max_dashboard_val.ToString();
                }
                else
                {
                    double_STR_To_Send = val.ToString();

                }
            }
            else
            {
                double_STR_To_Send = min_dashboard_val.ToString();
            }

            return double_STR_To_Send;
        }

        /**********************************/

        public void connect(string ip, int port)
        {
            try
            {
                _telnetClient.connect(ip, port);
                if (!_telnetClient.checkIfClientIsNull())
                {
                    this.ConnectionStatus = "Connected";
                    this.stop = false;
                    this.start();
                }
            }
            catch (Exception)
            {
                this.ConnectionStatus = "Disconnected";

                // NEED TO BE IMPLEMENTED THE REST  
            }
        }
        public void disconnect()
        {

            stop = true;
            _telnetClient.disconnect();
            this.ConnectionStatus = "Disconnected";
            this.restorebackTo();

        }
        public void FlyPlane(double rudder, double elevator)
        {
            try
            {
                m.WaitOne();
                StringBuilder sb = new StringBuilder(this.var_locations_in_simulator_send[2] + " " + elevator + "\n"); //build the command to set the elevator value in sim
                string elevatorCommand = sb.ToString();
                this._telnetClient.write(elevatorCommand);
                _telnetClient.read();
                sb = new StringBuilder(this.var_locations_in_simulator_send[1] + " " + rudder + "\n"); //build the command to set the rudder value in sim
                string rudderCommand = sb.ToString();
                this._telnetClient.write(rudderCommand);
                Console.WriteLine("elevatro:"+elevatorCommand);
                Console.WriteLine("rudder"+rudderCommand);
                _telnetClient.read();
                m.ReleaseMutex();

            }
            catch (Exception)
            {
                Console.WriteLine("problem with thread2");
                Console.WriteLine("could not send joystick values to simulator ");
            }
        }

        public void moveAileron(string aileron)
        {
            //if (connectionStatus == "Connected")
            //{
                try
                {
                    m.WaitOne();
                    StringBuilder sb = new StringBuilder(this.var_locations_in_simulator_send[3] + " " + aileron + "\n"); //build the command to set the aileron value in sim
                    string aileronCommand = sb.ToString();
                    this._telnetClient.write(aileronCommand);
                    Console.WriteLine(aileronCommand);
                    _telnetClient.read();
                    m.ReleaseMutex();
                }
                catch (Exception)
                {
                    Console.WriteLine("problem with thread2");
                    Console.WriteLine("could not send joystick values to simulator ");
                }
            //}

        }

        public void moveThrottle(double throttle)
        {
            //if (connectionStatus == "Connected")
            //{
                try
                {
                    m.WaitOne();
                    StringBuilder sb = new StringBuilder(this.var_locations_in_simulator_send[0] + " " + throttle + "\n"); //build the command to set the aileron value in sim
                    string throttleCommand = sb.ToString();
                    this._telnetClient.write(throttleCommand);
                    Console.WriteLine(throttleCommand);
                    _telnetClient.read();
                    m.ReleaseMutex();
                }
                catch (Exception)
                {
                    Console.WriteLine("problem with thread2");
                    Console.WriteLine("could not send joystick values to simulator ");
                }
            //}

        }

        public void start()
        {

            new Thread(delegate ()
            {
                while (!stop)
                {
                    if (this.ConnectionStatus != "Disconnected")
                    {
                        try
                        {
                            m.WaitOne();
                            float temp1 = 0;
                            double temp = 0;

                            _telnetClient.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                            //string val = _telnetClient.read().ToString();
                            if (float.TryParse(_telnetClient.read().ToString(), out temp1))
                            {
                                this.Indicated_heading_deg = temp1.ToString();
                                this.Indicated_heading_deg = checkThreshold_For_Dashboard_vars(Indicated_heading_deg);
                            }
                            else
                            {
                                this.Indicated_heading_deg = "ERR"; // there has been an error in parsing proccess of the variable from simulator
                                Console.WriteLine("there has been an error in parsing proccess of the variable " + this.Indicated_heading_deg + " from simulator");
                            }

                            _telnetClient.write("get /instrumentation/gps/indicated-vertical-speed\n");
                            if (float.TryParse(_telnetClient.read().ToString(), out temp1))
                            {
                                this.Gps_indicated_vertical_speed = temp1.ToString();
                                this.Gps_indicated_vertical_speed = checkThreshold_For_Dashboard_vars(Gps_indicated_vertical_speed);
                            }
                            else
                            {
                                this.Gps_indicated_vertical_speed = "ERR"; // there has been an error in parsing proccess of the variable from simulator
                                Console.WriteLine("there has been an error in parsing proccess of the variable " + this.Gps_indicated_vertical_speed + " from simulator");
                            }

                            _telnetClient.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                            if (float.TryParse(_telnetClient.read().ToString(), out temp1))
                            {
                                this.Gps_indicated_ground_speed_kt = temp1.ToString();
                                this.Gps_indicated_ground_speed_kt = checkThreshold_For_Dashboard_vars(Gps_indicated_ground_speed_kt);
                            }
                            else
                            {
                                this.Gps_indicated_ground_speed_kt = "ERR"; // there has been an error in parsing proccess of the variable from simulator
                                Console.WriteLine("there has been an error in parsing proccess of the variable " + this.Gps_indicated_ground_speed_kt + " from simulator");
                            }

                            _telnetClient.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                            if (float.TryParse(_telnetClient.read().ToString(), out temp1))
                            {
                                this.Airspeed_indicator_indicated_speed_kt = temp1.ToString();
                                this.Airspeed_indicator_indicated_speed_kt = checkThreshold_For_Dashboard_vars(Airspeed_indicator_indicated_speed_kt);
                            }
                            else
                            {
                                this.Airspeed_indicator_indicated_speed_kt = "ERR"; // there has been an error in parsing proccess of the variable from simulator
                                Console.WriteLine("there has been an error in parsing proccess of the variable " + this.Airspeed_indicator_indicated_speed_kt + " from simulator");
                            }

                            _telnetClient.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                            if (float.TryParse(_telnetClient.read().ToString(), out temp1))
                            {
                                this.Gps_indicated_altitude_ft = temp1.ToString();
                                this.Gps_indicated_altitude_ft = checkThreshold_For_Dashboard_vars(Gps_indicated_altitude_ft);
                            }
                            else
                            {
                                this.Gps_indicated_altitude_ft = "ERR"; // there has been an error in parsing proccess of the variable from simulator
                                Console.WriteLine("there has been an error in parsing proccess of the variable " + this.Gps_indicated_altitude_ft + " from simulator");
                            }

                            _telnetClient.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                            if (float.TryParse(_telnetClient.read().ToString(), out temp1))
                            {
                                this.Attitude_indicator_internal_roll_deg = temp1.ToString();
                                this.Attitude_indicator_internal_roll_deg = checkThreshold_For_Dashboard_vars(Attitude_indicator_internal_roll_deg);
                            }
                            else
                            {
                                this.Attitude_indicator_internal_roll_deg = "ERR"; // there has been an error in parsing proccess of the variable from simulator
                                Console.WriteLine("there has been an error in parsing proccess of the variable " + this.Attitude_indicator_internal_roll_deg + " from simulator");
                            }

                            _telnetClient.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                            if (float.TryParse(_telnetClient.read().ToString(), out temp1))
                            {
                                this.Attitude_indicator_internal_pitch_deg = temp1.ToString();
                                this.Attitude_indicator_internal_pitch_deg = checkThreshold_For_Dashboard_vars(Attitude_indicator_internal_pitch_deg);
                            }
                            else
                            {
                                this.Attitude_indicator_internal_pitch_deg = "ERR"; // there has been an error in parsing proccess of the variable from simulator
                                Console.WriteLine("there has been an error in parsing proccess of the variable " + this.Attitude_indicator_internal_pitch_deg + " from simulator");
                            }

                            _telnetClient.write("get /instrumentation/gps/indicated-altitude-ft\n");
                            if (float.TryParse(_telnetClient.read().ToString(), out temp1))
                            {
                                this.Altimeter_indicated_altitude_ft = temp1.ToString();
                                this.Altimeter_indicated_altitude_ft = checkThreshold_For_Dashboard_vars(Altimeter_indicated_altitude_ft);
                            }
                            else
                            {
                                this.Altimeter_indicated_altitude_ft = "ERR"; // there has been an error in parsing proccess of the variable from simulator
                                Console.WriteLine("there has been an error in parsing proccess of the variable " + this.Altimeter_indicated_altitude_ft + " from simulator");
                            }

                            _telnetClient.write("get /position/latitude-deg\n");
                            if (Double.TryParse(_telnetClient.read().ToString(), out temp))
                            {
                                this.Latitude_deg = temp.ToString();
                            }
                            else
                            {
                                this.Latitude_deg = "0"; // there has been an error in parsing proccess of the variable from simulator
                                Console.WriteLine("there has been an error in parsing proccess of the location of the latitude of the plane"
                                   + " from simulator, the default is set to 0");
                            }

                            _telnetClient.write("get /position/longitude-deg\n");
                            if (Double.TryParse(_telnetClient.read().ToString(), out temp))
                            {
                                this.Longitude_deg = temp.ToString();
                            }
                            else
                            {
                                this.Longitude_deg = "0"; // there has been an error in parsing proccess of the variable from simulator
                                Console.WriteLine("there has been an error in parsing proccess of the location of the longtitude of the plane"
                                   + " from simulator, the default is set to 0");
                            }

                            this.Locations = this.Latitude_deg + "," + this.Longitude_deg;
                            m.ReleaseMutex();
                        }
                        catch (TimeoutException){
                            ConnectionStatus = "Disconnected";
                            disconnect();
                            Console.WriteLine("Client has Disconnected from server due to server problem.");
                        }
                        // ALL Exceptions are thrown here
                        catch (Exception e)
                        {
                            ConnectionStatus = "Disconnected";
                            disconnect();
                            Console.WriteLine(e);
                        }

                    }

                    Thread.Sleep(250); // read data in 4Hz
                }
            }).Start();
        }
        public void restorebackTo()
        { // this function restores the value to default value "ERR", in case the client is disconnected from simulator
            Indicated_heading_deg = "####";
            Gps_indicated_vertical_speed = "####";
            Gps_indicated_ground_speed_kt = "####";
            Airspeed_indicator_indicated_speed_kt = "####";
            Gps_indicated_altitude_ft = "####"; //ALTIMETER = '/instrumentation/altimeter/indicated-altitude-ft'?
            Attitude_indicator_internal_roll_deg = "####";
            Attitude_indicator_internal_pitch_deg = "####";
            Altimeter_indicated_altitude_ft = "####";
        }
    }

}
