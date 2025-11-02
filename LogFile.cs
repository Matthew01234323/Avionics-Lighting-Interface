using System.IO;

namespace AircraftLightsGUI
{
    public class LogFile
    {
        private string file_path = "C:\\Users\\samho\\OneDrive\\Documents\\B&FC\\Year_2\\Object_Oriented_Programming\\Assignment_2\\Local_Repo\\Avionics-Lighting-Interface\\Avionics-Lighting-Interface\\Log_Files\\";
        private string file_name = "lights_logfile.txt";
        StreamWriter sw;
        
        public void WriteEvent(DateTime time, string id, string event_type)
        {
            sw.WriteLine($"{time} {id} {event_type}");
        }
    }
}