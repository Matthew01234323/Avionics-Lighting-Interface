using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircraft_Lights
{
    public class ExteriorLight : Light
    {
        private bool isExteriorLightAuto = true;
        private bool isFlashing = false;
        public ExteriorLight(string id) : base(id)
        {

        }
        public bool IsExteriorLightAuto
        {
            get { return isExteriorLightAuto; }
            set
            {
                isExteriorLightAuto = value;
                LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, $"Auto mode set to {isExteriorLightAuto}");
            }
        }
        public bool IsFlashing
        {
            get { return isFlashing; }
            set
            {
                isFlashing = value;
            }
        }

        // Operate light based on sunrise and sunset times if exterior light auto mode is enabled
        public void ExteriorLightAuto()
        {
            if (IsExteriorLightAuto)
            {
                TimeSpan currentTime = TimeInfo.CurrentTime;
                TimeSpan daytimeStart = TimeInfo.SunriseTime;
                TimeSpan daytimeEnd = TimeInfo.SunsetTime;

                if (currentTime >= daytimeStart && currentTime < daytimeEnd)
                {
                    base.TurnOff();
                    logFile.WriteEvent(FlightInfo.CurrentTime, LightId, "Auto turned OFF for daytime");
                }
                else
                {
                    base.TurnOn();
                    logFile.WriteEvent(FlightInfo.CurrentTime, LightId, "Auto turned ON for nighttime");
                }
            }
        }

        // Enable flashing mode based on whether the light is off or on
        public void EnableFlashing()
        {
            if (!IsOn)
            {
                IsFlashing = false;
                GUI.UpdateLightFlashingStatus(LightId, IsFlashing);
                LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "Cannot enable flashing when light is OFF");
            }
            else
            {
                IsFlashing = true;
                GUI.UpdateLightFlashingStatus(LightId, IsFlashing);
                LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "Flashing mode ENABLED");
            }
        }

        // Disable flashing mode
        public void DisableFlashing()
        {
            IsFlashing = false;
            GUI.UpdateLightFlashingStatus(LightId, IsFlashing);
            LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "Flashing mode DISABLED");
        }

        // Turn off light and disable flashing mode if active
        public override bool TurnOff()
        {
            base.TurnOff();

            if(IsFlashing)
            {
                IsFlashing = false;
                GUI.UpdateLightFlashingStatus(LightId, IsFlashing);
                LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "Flashing mode DISABLED due to light OFF");
            }

        }
    }
}

