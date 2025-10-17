using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircraft_Light_System
{
    public abstract class Light
    {
        private string lightId;
        private bool isOn = false;
        private bool isFault = false;
        private string colour;

        protected Light(string id)
        {
            lightId = id;
        }

        // Turn on light if no fault, update GUI and log event
        public virtual bool TurnOn()
        {
            if (!isFault)
            {
                isOn = true;
                GUI.UpdateLightStatus(lightId, isOn);
                LogFile.WriteEvent(FlightInfo.CurrentTime, lightId, "turned ON"); 
                return true;
            }
            return false;
        }

        // Turn off light, update GUI and log event
        public virtual bool TurnOff()
        {
            isOn = false;
            GUI.UpdateLightStatus(lightId, isOn);
            LogFile.WriteEvent(FlightInfo.CurrentTime, lightId, "turned OFF");
            return true;
        }

        // Sets fault status and turns off light if fault is true, and log event
        public void HasFault(bool faultStatus)
        {
            isFault = faultStatus;
            if (isFault && isOn)
            {
                TurnOff();
            }
            LogFile.WriteEvent(FlightInfo.CurrentTime, lightId, "FAULT detected ");
        }
    }
}