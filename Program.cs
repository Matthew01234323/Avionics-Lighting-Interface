using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace AircraftLightsGUI
{
    static class Program
    {
        static public bool InFlight = false;
        static void Main()
        {
            FlightInfo.ReadFlightInfo();

            while(InFlight)
            {
                
            }
        }
    }

    
}