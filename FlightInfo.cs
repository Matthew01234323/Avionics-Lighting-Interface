using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Text.Json;


namespace AircraftLightsGUI
{
    public static class FlightInfo
    {
        static DateTime takeoff_time;
        static DateTime landing_time;
        static DateTime sunset_time;
        static DateTime sunrise_time;
        static string json_filepath = "C:\\Users\\samho\\OneDrive\\Documents\\B&FC\\Year 2\\Object Oriented Programming\\Assignment 2\\Local_Repo\\Avionics-Lighting-Interface\\Avionics-Lighting-Interface\\Flight_Info_File\\";
        static string json_filename = "flight_data.json";
        static public DateTime current_time;

        static Random rnd = new Random();

        //static public List<ExteriorLight> exterior_lights_list = new List<ExteriorLight>();
        //static public List<DimmingLight> dimming_lights_list = new List<DimmingLight>();
        //static public List<AsileLight> exterior_lights_list = new List<AsileLight>();
        //static public List<InteriorLight> exterior_lights_list = new List<InteriorLight>();
        //static public List<Light> all_lights_list = new List<Light>();

        public static void ReadFlightInfo()
        {
            try
            {
                using FileStream json_stream = new FileStream($"{json_filepath}{json_filename}", FileMode.Open, FileAccess.Read);
                using JsonDocument doc = JsonDocument.Parse(json_stream);
                JsonElement root = doc.RootElement;

                takeoff_time = root.GetProperty("takeoff_time").GetDateTime();
                landing_time = root.GetProperty("landing_time").GetDateTime();
                sunset_time = root.GetProperty("sunset_time").GetDateTime();
                sunrise_time = root.GetProperty("sunrise_time").GetDateTime();

                current_time = takeoff_time;

                Program.InFlight = true;

                LogFile.WriteEvent(current_time, "System", "Flight Info read successfully");
            }
            catch (Exception e)
            {
                LogFile.WriteEvent(DateTime.Now, "System", $"Error when reading flight info file: {e}");
            }
        }
        
        static public void CheckEvents()
        {
            int rnd_value;

            if (DateTime.Compare(current_time, landing_time) >= 0)
            {
                LogFile.WriteEvent(current_time, "System", "Plane has landed");
                Program.InFlight = false;
            }
            else
            {
                rnd_value = rnd.Next(1, 101);
                LogFile.WriteEvent(current_time, "System", $"random value is {rnd_value}");
            }

            current_time = current_time.AddMinutes(5);
        }
    }
}