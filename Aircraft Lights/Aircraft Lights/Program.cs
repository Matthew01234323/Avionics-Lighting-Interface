using System;

namespace Aircraft_Lights
{
    // Mock classes for dependencies
    public static class GUI
    {
        public static void UpdateLightStatus(string lightId, bool status)
        {
            Console.WriteLine($"  [GUI] Light {lightId} status updated to: {status}");
        }
    }

    public static class LogFile
    {
        public static void WriteEvent(string time, string lightId, string message)
        {
            Console.WriteLine($"  [LOG] {time} | {lightId} | {message}");
        }
    }

    public static class FlightInfo
    {
        public static string CurrentTime => DateTime.Now.ToString("HH:mm:ss");
    }

    // Concrete implementation for testing
    public class TestInteriorLight : InteriorLight
    {
        public TestInteriorLight(string id) : base(id) { }
    }

    // Test Program
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== INTERIOR LIGHT TESTING ===\n");

            TestInteriorLight light = new TestInteriorLight("AISLE-001");

            // Test 1: Initial State
            Console.WriteLine("TEST 1: Check Initial State");
            Console.WriteLine($"  Disabled: {light.Disabled}");
            Console.WriteLine($"  Emergency Mode: {light.EmergencyModeStatus}");
            Console.WriteLine($"  Colour: {light.Colour}");
            Console.WriteLine($"  Is On: {light.IsOn}");
            Console.WriteLine($"  Has Fault: {light.IsFault}\n");

            // Test 2: Turn On
            Console.WriteLine("TEST 2: Turn On Light");
            bool result = light.TurnOn();
            Console.WriteLine($"  Turn On Result: {result}");
            Console.WriteLine($"  Is On: {light.IsOn}\n");

            // Test 3: Change Colour
            Console.WriteLine("TEST 3: Change Colour to Blue");
            light.SetColour("Blue");
            Console.WriteLine($"  Colour: {light.Colour}\n");

            // Test 4: Turn Off
            Console.WriteLine("TEST 4: Turn Off Light");
            result = light.TurnOff();
            Console.WriteLine($"  Turn Off Result: {result}");
            Console.WriteLine($"  Is On: {light.IsOn}\n");

            // Test 5: Disable Light
            Console.WriteLine("TEST 5: Disable Light");
            light.Disable();
            Console.WriteLine($"  Disabled: {light.Disabled}");
            Console.WriteLine($"  Is On: {light.IsOn}\n");

            // Test 6: Try to Turn On While Disabled
            Console.WriteLine("TEST 6: Try to Turn On While Disabled");
            result = light.TurnOn();
            Console.WriteLine($"  Turn On Result: {result} (Should be False)");
            Console.WriteLine($"  Is On: {light.IsOn}\n");

            // Test 7: Enable Light
            Console.WriteLine("TEST 7: Enable Light");
            light.Enable();
            Console.WriteLine($"  Disabled: {light.Disabled}\n");

            // Test 8: Turn On Again
            Console.WriteLine("TEST 8: Turn On Light Again");
            result = light.TurnOn();
            Console.WriteLine($"  Turn On Result: {result}");
            Console.WriteLine($"  Is On: {light.IsOn}\n");

            // Test 9: Emergency Mode On (While Light is Already On and Enabled)
            Console.WriteLine("TEST 9: Turn On Emergency Mode (Light Already On)");
            result = light.EmergencyModeOn();
            Console.WriteLine($"  Emergency On Result: {result}");
            Console.WriteLine($"  Emergency Mode: {light.EmergencyModeStatus}");
            Console.WriteLine($"  Colour: {light.Colour}");
            Console.WriteLine($"  Is On: {light.IsOn}");
            Console.WriteLine($"  Disabled: {light.Disabled}\n");

            // Test 10: Turn Off Emergency Mode
            Console.WriteLine("TEST 10: Turn Off Emergency Mode");
            light.EmergencyModeOff();
            Console.WriteLine($"  Emergency Mode: {light.EmergencyModeStatus}");
            Console.WriteLine($"  Disabled: {light.Disabled}");
            Console.WriteLine($"  Colour: {light.Colour}");
            Console.WriteLine($"  Is On: {light.IsOn}\n");

            // Test 11: Disable Then Emergency Mode (Testing Override)
            Console.WriteLine("TEST 11: Disable, Then Emergency Mode (Testing Override)");
            light.Disable();
            Console.WriteLine($"  After Disable - Is On: {light.IsOn}, Disabled: {light.Disabled}");
            result = light.EmergencyModeOn();
            Console.WriteLine($"  After Emergency On - Is On: {light.IsOn}, Emergency: {light.EmergencyModeStatus}");
            Console.WriteLine($"  Colour: {light.Colour}\n");

            // Test 12: Emergency Off (Should Re-enable)
            Console.WriteLine("TEST 12: Turn Off Emergency Mode (Should Re-enable)");
            light.EmergencyModeOff();
            Console.WriteLine($"  Emergency Mode: {light.EmergencyModeStatus}");
            Console.WriteLine($"  Disabled: {light.Disabled}");
            Console.WriteLine($"  Colour: {light.Colour}");
            Console.WriteLine($"  Is On: {light.IsOn}\n");

            Console.WriteLine("=== TESTING COMPLETE ===");
            Console.ReadLine();
        }
    }
}