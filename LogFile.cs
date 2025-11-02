using System.IO;

namespace AircraftLightsGUI
{
    public class LogFile
    {
        private string file_path = "C:\\Users\\samho\\OneDrive\\Documents\\";
        private string file_name = "lights_logfile.txt";

        
        
        public void WriteEvent(DateTime time, string id, string event_type)
        {
            try
            {
                FileStream stream = new($"{file_path}{file_name}", FileMode.Append);
                
                StreamWriter sw = new StreamWriter(stream);

                sw.WriteLine($"{time} {id} {event_type}");
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}