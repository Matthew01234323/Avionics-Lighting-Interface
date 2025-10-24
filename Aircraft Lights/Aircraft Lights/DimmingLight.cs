﻿using System;
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

        public int Brightness
        {
            get { return brightness; }
            set { brightness = value; }
        }
        public DimmingLight(string id) : base(id)
        {

        }

        // Set brightness within valid range, update GUI and log event
        public void SetBrightness(int value)
        {
            if (value >= 1 && value <= 10)
            {
                Brightness = value;
                GUI.UpdateLightStatus(LightId, IsOn, IsFault, IsDisabled, IsEmergency, Colour, Brightness);
                LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, $"brightness set to {Brightness}");
            }
            else
            {
                LogFile.WriteEvent(FlightInfo.CurrentTime, LightId, $"invalid brightness value: {value}");
            }
        }

    }
}



