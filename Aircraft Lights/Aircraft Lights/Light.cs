﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircraft_Lights
{
    public abstract class Light
    {
        private string lightId;
        private bool isOn = false;
        private bool isFault = false;

        protected Light(string id)
        {
            lightId = id;
        }
        public string LightId
        {
            get { return lightId; }
            set { lightId = value; }
        }
        public bool IsOn
        {
            get { return isOn; }
            set { isOn = value; }
        }

        public bool IsFault
        { 
            get { return isFault; }
            set { isFault = value; }
        }

        // Turn on light if no fault, update GUI and log event
        public virtual bool TurnOn()
        {
            if (!IsFault)
            {
                IsOn = true;
                GUI.UpdateLightStatus(LightId, IsOn, IsFault);
                LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "turned ON"); 
                return true;
            }
            return false;
        }

        // Turn off light, update GUI and log event
        public virtual bool TurnOff()
        {
            IsOn = false;
            GUI.UpdateLightStatus(LightId, IsOn, IsFault);
            LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "turned OFF");
            return true;
        }

        // Sets fault status and turns off light if fault is true, update GUI and log event
        public void HasFault(bool faultStatus)
        {
            IsFault = faultStatus;
            if (IsFault && IsOn)
            {
                TurnOff();
            }
            GUI.UpdateLightStatus(LightId, IsOn, IsFault);
            LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "FAULT detected ");
        }
    }
}