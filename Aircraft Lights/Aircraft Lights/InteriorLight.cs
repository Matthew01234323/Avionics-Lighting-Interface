﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircraft_Lights
{
    public abstract class InteriorLight : Light
    {
        private bool isDisabled = false;
        private bool isEmergency = false;
        private string colour = "White";

        public bool IsDisabled
        {
            get { return isDisabled; }
            set { isDisabled = value; }
        }

        public bool IsEmergency
        {
            get { return isEmergency; }
            set { isEmergency = value; }
        }

        public string Colour
        {
            get { return colour; }
            set { colour = value; }
        }

        public InteriorLight(string id) : base(id)
        {

        }

        // Disable local control of the light, update GUI and log event
        public void Disable()
        {
            IsDisabled = true;
            base.TurnOff();
            GUI.UpdateLightStatus(LightId, IsOn, IsFault, IsDisabled, IsEmergency, Colour);
            LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "DISABLED");
        }

        // Enable local control of the light, update GUI and log event
        public void Enable()
        {
            IsDisabled = false;
            GUI.UpdateLightStatus(LightId, IsOn, IsFault, IsDisabled, IsEmergency, Colour);
            LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "ENABLED");
        }

        // Control TurnOn based on disabled status and log event
        public override bool TurnOn()
        {
            if (!IsDisabled)
            {
                return base.TurnOn();
            }
            else
            {
                LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "Turn ON blocked - light is DISABLED");
                return false;
            }
        }

        // Set the colour of the light, log event and update GUI
        public void SetColour(string newColour)
        {
            Colour = newColour;
            GUI.UpdateLightStatus(LightId, IsOn, IsFault, IsDisabled, IsEmergency, Colour);
            LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "Colour set to " + newColour);
        }
        // Activate emergency mode: set colour to Red, update GUI and log event

        public bool EmergencyModeOn()
        {
            
            IsEmergency = true;
            SetColour("Red");
            GUI.UpdateLightStatus(LightId, IsOn, IsFault, IsDisabled, IsEmergency, Colour);
            LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "Set to Emergency Mode");
            return base.TurnOn();
                  
        }

        // Deactivate emergency mode: set colour to White, enable light, update GUI and log event
        public void EmergencyModeOff()
        {
            IsEmergency = false;
            IsDisabled = false;
            SetColour("White");
            GUI.UpdateLightStatus(LightId, IsOn, IsFault, IsDisabled, IsEmergency, Colour);
            LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "Emergency Mode OFF, colour set to White, light ENABLED");
        }
    }
}

