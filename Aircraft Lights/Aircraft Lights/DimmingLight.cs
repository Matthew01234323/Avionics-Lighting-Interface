using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircraft_Lights
{
    public class DimmingLight : InteriorLight
    {
        // Brightness level from 1 (dim) to 10 (bright), default is 5
        private int brightness = 5;

        protected DimmingLight(string id) : base(id)
        {

        }
        public int Brightness
        {
            get { return brightness; }

            // Set brightness within valid range and log event
            set
            {
                if (value >= 1 && value <= 10)
                {
                    brightness = value;
                    LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, $"brightness set to {brightness}");
                }
            }

        }
    }
}


