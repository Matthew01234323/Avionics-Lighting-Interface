

namespace AircraftLightsGUI
{
    public class LogFile
    {
        private string file_path = "C:\\Users\\samho\\OneDrive\\Documents\\B&FC\\Year_2\\Object_Oriented_Programming\\Assignment_2\\Local_Repo\\Avionics-Lighting-Interface\\Avionics-Lighting-Interface\\Log_Files\\";
        private string file_name = "";

        void SetFileName()
        {
            file_name = $"Flight_{flight-info.flight_number}";
        }
        
        void WriteEvent(DateTime time, string id, string event_type)
        {
            
        }
    }
}