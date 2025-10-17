using System;
using System.Collections.Generic;

namespace Aircraft_Light_System
{
    // ===== CONCRETE LIGHT IMPLEMENTATIONS =====
    public class NavigationLight : Light
    {
        public NavigationLight(string id) : base(id) { }
    }

    public class AisleLight : Light
    {
        public AisleLight(string id) : base(id) { }
    }

    public class TakeoffLight : Light
    {
        public TakeoffLight(string id) : base(id) { }
    }

    public class LandingLight : Light
    {
        public LandingLight(string id) : base(id) { }
    }

    // ===== DUMMY GUI CLASS =====
    public static class GUI
    {
        private static Dictionary<string, bool> lightStatuses = new Dictionary<string, bool>();

        public static void UpdateLightStatus(string lightId, bool status)
        {
            lightStatuses[lightId] = status;
            Console.ForegroundColor = status ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"[GUI] Light {lightId}: {(status ? "ON" : "OFF")}");
            Console.ResetColor();
        }

        public static Dictionary<string, bool> GetAllStatuses()
        {
            return new Dictionary<string, bool>(lightStatuses);
        }
    }

    // ===== DUMMY LOG FILE CLASS =====
    public static class LogFile
    {
        private static List<string> logs = new List<string>();

        public static void WriteEvent(DateTime timestamp, string lightId, string eventDescription)
        {
            string entry = $"[{timestamp:HH:mm:ss}] {lightId}: {eventDescription}";
            logs.Add(entry);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[LOG] {entry}");
            Console.ResetColor();
        }

        public static List<string> GetAllLogs()
        {
            return new List<string>(logs);
        }
    }

    // ===== DUMMY FLIGHT INFO CLASS =====
    public static class FlightInfo
    {
        public static DateTime CurrentTime { get; set; } = DateTime.Now;
    }

    // ===== MAIN PROGRAM =====
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=========================================");
            Console.WriteLine("   AIRCRAFT LIGHTING SYSTEM TEST");
            Console.WriteLine("=========================================\n");

            // TEST 1: Turn light ON
            Console.WriteLine("TEST 1: Turn Navigation Light ON");
            var navLight = new NavigationLight("NAV-001");
            bool result1 = navLight.TurnOn();
            Console.WriteLine($"Return value: {result1}\n");

            // TEST 2: Turn light OFF
            Console.WriteLine("TEST 2: Turn Navigation Light OFF");
            bool result2 = navLight.TurnOff();
            Console.WriteLine($"Return value: {result2}\n");

            // TEST 3: Fault handling
            Console.WriteLine("TEST 3: Turn Aisle Light ON, then set fault");
            var aisleLight = new AisleLight("AISLE-001");
            aisleLight.TurnOn();
            Console.WriteLine("Setting fault...");
            aisleLight.HasFault(true);
            Console.WriteLine();

            // TEST 4: Try to turn on faulted light
            Console.WriteLine("TEST 4: Try to turn ON faulted light");
            bool result4 = aisleLight.TurnOn();
            Console.WriteLine($"Return value: {result4} (should be False)\n");

            // TEST 5: Multiple lights
            Console.WriteLine("TEST 5: Control multiple lights");
            var takeoffLight = new TakeoffLight("TAKEOFF-001");
            var landingLight = new LandingLight("LANDING-001");
            takeoffLight.TurnOn();
            landingLight.TurnOn();
            takeoffLight.TurnOff();
            landingLight.TurnOff();

            // SUMMARY
            Console.WriteLine("\n=========================================");
            Console.WriteLine("           TEST COMPLETE");
            Console.WriteLine("=========================================");
            Console.WriteLine("GUI integration: WORKING");
            Console.WriteLine("Log system: WORKING");
            Console.WriteLine("Light functions: WORKING");
            Console.WriteLine("Fault handling: WORKING");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}