namespace FlightSimulatorApp.Model
{
    internal interface IconnectionToserver
    {
        void connect(string ip, int port);
        void disconnect();
        void start();
    }
}