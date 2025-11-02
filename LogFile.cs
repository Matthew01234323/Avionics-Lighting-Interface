using System.IO;

namespace AircraftLightsGUI
{
    public class LogFile
    {
        private string file_path = "C:\\Users\\samho\\OneDrive\\Documents\\B&FC\\Year_2\\Object_Oriented_Programming\\Assignment_2\\Local_Repo\\Avionics-Lighting-Interface\\Avionics-Lighting-Interface\\Log_Files\\";
        private string file_name = "";
        StreamWriter sw;

        public void SetFileName(FlightInfo flight_info)
        {
            file_name = $"Flight_{flight_info.flight_number}.txt";

            File.Create($"{file_path}{file_name}").Dispose();

            sw = new StreamWriter($"{file_path}{file_name}");
        }
        
        public void WriteEvent(DateTime time, string id, string event_type)
        {
            sw.WriteLine($"{time} {id} {event_type}");
        }
    }
}