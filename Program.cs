using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AircraftLightsGUI
{
    static class Program
    {
        static public bool InFlight = false;
        static async Task Main()
        {
            FlightInfo.ReadFlightInfo();

            while(InFlight)
            {
                await Task.Delay(2000);
                FlightInfo.CheckEvents();
            }
        }
    }

    
}