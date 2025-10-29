using System;
using System.Runtime.CompilerServices;
using AircraftLightsGUI;


namespace AircraftLightsGUI
{
    // Static class to hold current time, sunrise time, and sunset time information
    public static class TimeInfo
    {
        public static TimeSpan CurrentTime { get; set; }
        public static TimeSpan SunriseTime { get; set; } 
        public static TimeSpan SunsetTime { get; set; } 
    }

    // Static class to simulate GUI updates
    public static class GUI
    {
        public static void UpdateLightStatus(params object[] args)
        {
            Console.WriteLine($"  Data sent to GUI: {string.Join(", ", args)}");
            Console.WriteLine();
        }
    }

    // Static class to simulate logging events to log file
    public static class LogFile
    {
        public static void WriteEvent(params object[] args)
        {
            Console.WriteLine($"  Data sent to LogFile: {string.Join(", ", args)}");
            Console.WriteLine();
        }
    }

    // Static class to simulate FlightInfo to supply LogFile with current time
    public static class FlightInfo
    {
        public static DateTime CurrentTime { get; set; } = DateTime.Now;
    }
}

class TestProgram
{
    static void Main(string[] args)
    {   
        
        // Test abstract Light class using DimmingLight class

        Console.WriteLine("Testing abstract Light class using DimmingLight class");
        Console.WriteLine("---------------------------------------------------");
        Console.WriteLine();

        // Instantiate a DimmingLight object for testing
        DimmingLight dimmingLight = new DimmingLight("DL");

        
        dimmingLight.IsFault = false;
        dimmingLight.IsOn = false;
        dimmingLight.TurnOn();
        Console.WriteLine($"IsOn = {dimmingLight.IsOn}");
        Console.WriteLine();

        dimmingLight.IsFault = true;
        dimmingLight.IsOn = false;
        dimmingLight.TurnOn();
        dimmingLight.TurnOn();
        Console.WriteLine($"IsOn = {dimmingLight.IsOn}");
        Console.WriteLine();

        dimmingLight.IsOn = true;
        dimmingLight.TurnOff();
        Console.WriteLine($"IsOn = {dimmingLight.IsOn}");
        Console.WriteLine();

        dimmingLight.IsOn = true;
        dimmingLight.IsFault = false;
        dimmingLight.HasFault(true);
        Console.WriteLine($"IsFault = {dimmingLight.IsFault}");
        Console.WriteLine($"IsOn = {dimmingLight.IsOn}");
        Console.WriteLine();  
        
        dimmingLight.IsOn= false;
        dimmingLight.IsFault = false;
        dimmingLight.HasFault(true);
        Console.WriteLine($"IsFault = {dimmingLight.IsFault}");
        Console.WriteLine($"IsOn = {dimmingLight.IsOn}");
        Console.WriteLine();

        

        // Test abstract InteriorLight class using DimmingLight class

        Console.WriteLine("Testing abstract InteriorLight class using DimmingLight class");
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine();

        dimmingLight.IsFault = false;
        dimmingLight.IsDisabled = false;
        dimmingLight.IsOn = true;
        dimmingLight.Disable();
        Console.WriteLine($"IsFault = {dimmingLight.IsFault}");
        Console.WriteLine($"IsDisabled = {dimmingLight.IsDisabled}");
        Console.WriteLine($"IsOn = {dimmingLight.IsOn}");
        Console.WriteLine();

        dimmingLight.IsDisabled = false;
        dimmingLight.IsOn = false;
        dimmingLight.Disable();
        Console.WriteLine($"IsDisabled = {dimmingLight.IsDisabled}");
        Console.WriteLine($"IsOn = {dimmingLight.IsOn}");
        Console.WriteLine();    

        dimmingLight.IsDisabled = true;
        dimmingLight.IsOn = false;
        dimmingLight.Enable();
        Console.WriteLine($"IsDisabled = {dimmingLight.IsDisabled}");
        Console.WriteLine($"IsOn = {dimmingLight.IsOn}");
        Console.WriteLine();

        dimmingLight.IsDisabled = true;
        dimmingLight.IsOn = false;
        dimmingLight.TurnOn();
        Console.WriteLine($"IsDisabled = {dimmingLight.IsDisabled}");
        Console.WriteLine($"IsOn = {dimmingLight.IsOn}");
        Console.WriteLine();

        dimmingLight.SetColour("Blue");
        Console.WriteLine($"Colour = {dimmingLight.Colour}");
        Console.WriteLine();

        dimmingLight.IsDisabled = false;
        dimmingLight.IsOn = false;
        dimmingLight.IsEmergency = false;
        dimmingLight.EmergencyModeOn();
        Console.WriteLine($"IsOn = {dimmingLight.IsOn}");
        Console.WriteLine($"IsEmergency = {dimmingLight.IsEmergency}");
        Console.WriteLine($"Colour = {dimmingLight.Colour}");
        Console.WriteLine();

        dimmingLight.EmergencyModeOff();
        Console.WriteLine($"IsOn = {dimmingLight.IsOn}");
        Console.WriteLine($"IsEmergency = {dimmingLight.IsEmergency}");
        Console.WriteLine($"Colour = {dimmingLight.Colour}");
        Console.WriteLine();

        dimmingLight.Disable();
        dimmingLight.EmergencyModeOn();
        Console.WriteLine($"IsOn = {dimmingLight.IsOn}");
        Console.WriteLine($"IsEmergency = {dimmingLight.IsEmergency}");
        Console.WriteLine($"IsDisabled = {dimmingLight.IsDisabled}");
        Console.WriteLine($"Colour = {dimmingLight.Colour}");
        Console.WriteLine();
        

        // Test DimmingLight class
        Console.WriteLine("Testing DimmingLight class");
        Console.WriteLine("------------------------");
        Console.WriteLine();

        dimmingLight.SetBrightness(1);
        Console.WriteLine($"Brightness = {dimmingLight.Brightness}");
        Console.WriteLine();

        dimmingLight.SetBrightness(10);
        Console.WriteLine($"Brightness = {dimmingLight.Brightness}");
        Console.WriteLine();

        
        // Test AisleLight class
        Console.WriteLine("Testing AisleLight class");
        Console.WriteLine("----------------------");
        Console.WriteLine();

        // Instantiate an AisleLight object for testing
        AisleLight aisleLight = new AisleLight("AL");

        aisleLight.AutoBrightnessEnabled = true;
        aisleLight.DisableAutoBrightness();
        Console.WriteLine($"AutoBrightnessEnabled = {aisleLight.AutoBrightnessEnabled}");
        Console.WriteLine();

        aisleLight.AutoBrightnessEnabled = false;
        aisleLight.EnableAutoBrightness();
        Console.WriteLine($"AutoBrightnessEnabled = {aisleLight.AutoBrightnessEnabled}");
        Console.WriteLine();

        TimeInfo.CurrentTime = new TimeSpan(23, 0, 0); // 23:00 
        TimeInfo.SunriseTime = new TimeSpan(6, 0, 0);  // 06:00
        TimeInfo.SunsetTime = new TimeSpan(18, 0, 0);  // 18:00
        Console.WriteLine($"Previous value for: Brightness = {aisleLight.Brightness}");
        aisleLight.AdjustForTimeOfDay();
        Console.WriteLine($"New value for: Brightness = {aisleLight.Brightness}");
        Console.WriteLine();

        TimeInfo.CurrentTime = new TimeSpan(13, 0, 0); // 13:00 
        TimeInfo.SunriseTime = new TimeSpan(6, 0, 0);  // 06:00
        TimeInfo.SunsetTime = new TimeSpan(18, 0, 0);  // 18:00
        Console.WriteLine($"Previous value for: Brightness = {aisleLight.Brightness}");
        aisleLight.AdjustForTimeOfDay();
        Console.WriteLine($"New value for: Brightness = {aisleLight.Brightness}");
        Console.WriteLine();

        // Test ExteriorLight class
        Console.WriteLine("Testing ExteriorLight class");
        Console.WriteLine("-------------------------");
        Console.WriteLine();

        
        // Instantiate an ExteriorLight object for testing
        ExteriorLight exteriorLight = new ExteriorLight("EL");

        exteriorLight.IsExteriorLightAuto = false;
        exteriorLight.EnableExteriorLightAuto();
        Console.WriteLine($"IsExteriorLightAuto = {exteriorLight.IsExteriorLightAuto}");
        Console.WriteLine();

        exteriorLight.DisableExteriorLightAuto();
        Console.WriteLine($"IsExteriorLightAuto = {exteriorLight.IsExteriorLightAuto}");
        Console.WriteLine();

        exteriorLight.IsOn = false;
        exteriorLight.IsExteriorLightAuto = true;
        TimeInfo.CurrentTime = new TimeSpan(23, 0, 0); // 23:00 
        TimeInfo.SunriseTime = new TimeSpan(6, 0, 0);  // 06:00
        TimeInfo.SunsetTime = new TimeSpan(18, 0, 0);  // 18:00
        exteriorLight.ExteriorLightAuto();
        Console.WriteLine($"IsOn = {exteriorLight.IsOn}");
        Console.WriteLine();

        exteriorLight.IsOn = true;
        exteriorLight.IsExteriorLightAuto = true;
        TimeInfo.CurrentTime = new TimeSpan(13, 0, 0); // 13:00 
        TimeInfo.SunriseTime = new TimeSpan(6, 0, 0);  // 06:00
        TimeInfo.SunsetTime = new TimeSpan(18, 0, 0);  // 18:00
        exteriorLight.ExteriorLightAuto();
        Console.WriteLine($"IsOn = {exteriorLight.IsOn}");
        Console.WriteLine();

        exteriorLight.IsFlashing = false;
        exteriorLight.IsOn = true;
        exteriorLight.EnableFlashing();
        Console.WriteLine($"IsFlashing = {exteriorLight.IsFlashing}");
        Console.WriteLine($"IsOn = {exteriorLight.IsOn}");
        Console.WriteLine();

        exteriorLight.IsFlashing = false;
        exteriorLight.IsOn = false;
        exteriorLight.EnableFlashing();
        Console.WriteLine($"IsFlashing = {exteriorLight.IsFlashing}");
        Console.WriteLine($"IsOn = {exteriorLight.IsOn}");
        Console.WriteLine();

        exteriorLight.IsFlashing = true;
        exteriorLight.IsOn = true;
        exteriorLight.DisableFlashing();
        Console.WriteLine($"IsFlashing = {exteriorLight.IsFlashing}");
        Console.WriteLine($"IsOn = {exteriorLight.IsOn}");
        Console.WriteLine();

        exteriorLight.IsFlashing = true;
        exteriorLight.IsOn = true;
        exteriorLight.TurnOff();
        Console.WriteLine($"IsFlashing = {exteriorLight.IsFlashing}");
        Console.WriteLine($"IsOn = {exteriorLight.IsOn}");
        Console.WriteLine();





    }


}

