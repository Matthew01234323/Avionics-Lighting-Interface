

namespace AircraftLightsGUI
{
    public class LogFile
    {
        private string file_path = "C:\\Users\\samho\\OneDrive\\Documents\\B&FC\\Year_2\\Object_Oriented_Programming\\Assignment_2\\Local_Repo\\Avionics-Lighting-Interface\\Avionics-Lighting-Interface\\Log_Files\\";
        private string file_name = "";

        public void SetFileName(FlightInfo flight_info)
        {
            file_name = $"Flight_{flight_info.flight_number}";
        }
        
        public void WriteEvent(DateTime time, string id, string event_type)
        {
            
        }
    }
}