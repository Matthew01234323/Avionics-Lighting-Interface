using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircraft_Lights
{
    public class AisleLight : DimmingLight
    {
        private bool autoBrightnessEnabled = true;
        public AisleLight(string id) : base(id)
        {

        }
        public bool AutoBrightnessEnabled
        {
            get { return autoBrightnessEnabled; }
            set
            {
                autoBrightnessEnabled = value;
                LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, $"Auto-brightness set to {autoBrightnessEnabled}");
            }
        }

        // Adjust brightness based on sunrise and sunset times if auto-brightness is enabled
        public void AdjustForTimeOfDay()
        {
            if (autoBrightnessEnabled && IsOn)
            {
                TimeSpan currentTime = TimeInfo.CurrentTime;
                TimeSpan daytimeStart = TimeInfo.SunriseTime;
                TimeSpan daytimeEnd = TimeInfo.SunsetTime;

                if (currentTime >= daytimeStart && currentTime < daytimeEnd)
                {
                    Brightness = 5;
                }
                else
                {
                    Brightness = 2;
                }

                LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, $"Adjusting brightness for time of day to {Brightness}");

            }
        }
    }
}
    

