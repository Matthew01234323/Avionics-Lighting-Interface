using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace AircraftLightsGUI
{
    class Program
    {
        static void Main()
        {
            FlightInfo Flight_Info = new();
            LogFile Log_File = new();

            Log_File.WriteEvent(DateTime.Now, "Test", "testing");
            Log_File.WriteEvent(DateTime.Now, "Test2", "still testing");
        }
    }

    
}