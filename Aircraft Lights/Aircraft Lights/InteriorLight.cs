using Aircraft_Lights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Aircraft_Lights
{
    public abstract class InteriorLight : Light
    {
        private bool disabled = false;
        private bool isEmergencyMode = false;
        private string colour = "White";

        public bool Disabled
        {
            get { return disabled; }
        }

        public bool EmergencyModeStatus
        {
            get { return isEmergencyMode; }
        }

        public string Colour
        {
            get { return colour; }
        }

        public InteriorLight(string id) : base(id)
        {

        }

        // Disable local control of the light, update GUI and log event
        public void Disable()
        {
            disabled = true;
            base.TurnOff();
            GUI.UpdateLightStatus(LightId, disabled);
            LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "DISABLED");
        }

        // Enable local control of the light, update GUI and log event
        public void Enable()
        {
            disabled = false;
            GUI.UpdateLightStatus(LightId, disabled);
            LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "ENABLED");
        }

        // Control TurnOn based on disabled status and log event
        public override bool TurnOn()
        {
            if (!disabled)
            {
                return base.TurnOn();
            }
            else
            {
                LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "Turn ON blocked - light is DISABLED");
                return false;
            }
        }

        // Set the colour of the light and log event
        public void SetColour(string colour)
        {
            this.colour = colour;
            LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "Colour set to " + colour);
        }
        // Activate emergency mode: set colour to Red, update GUI and log event

        public bool EmergencyModeOn()
        {
            
            isEmergencyMode = true;
            SetColour("Red");
            GUI.UpdateLightStatus(LightId, isEmergencyMode);
            LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "Set to Emergency Mode");
            return base.TurnOn();
                  
        }

        // Deactivate emergency mode: set colour to White, enable light, update GUI and log event
        public void EmergencyModeOff()
        {
            isEmergencyMode = false;
            disabled = false;
            SetColour("White");
            GUI.UpdateLightStatus(LightId, isEmergencyMode);
            GUI.UpdateLightStatus(LightId, disabled);
            LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, "Emergency Mode OFF, colour set to White, light ENABLED");
        }
    }
}

