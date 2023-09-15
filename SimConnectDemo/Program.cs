using System;
using Microsoft.FlightSimulator.SimConnect;
using System.Runtime.InteropServices;

namespace SimConnectDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting SimConnect Connection");
            SimConnect simConnect = null;
            const int WM_USER_SIMCONNECT = 0x0402;

            try
            {
                // Create IntPtr window handle.
                IntPtr handle = new IntPtr(0);
                simConnect = new SimConnect("Managed Data Request", handle, WM_USER_SIMCONNECT, null, 0);
                simConnect.OnRecvOpen += SimConnect_RecvOpenEventHandler;
                Console.WriteLine("Connected to SimConnect");

                bool tryAgain = true;

                // Create a while loop while tryAgain is true, it will keep trying to call FlightPlan Load, else it will exit.
                do
                {
                    string targetedFlightPlan;

                    // If the user presses '1', it will load the test.pln file.
                    // If the user presses '2', it will load the test2.pln file.
                    Console.WriteLine("Press '1' to load the Yakima route, '2' to load the Spokane route, or '3' to exit:");
                    
                    // Get the user input.
                    string userInput = Console.ReadLine();

                    // If the user presses '1', it will load the test.pln file.
                    if (userInput == "1")
                    {
                        // Set the targeted flight plan to the Yakima route.
                        targetedFlightPlan = "C:/test";
                    }

                    // If the user presses '2', it will load the test2.pln file.
                    else if (userInput == "2")
                    {
                        // Set the targeted flight plan to the Spokane route.
                        targetedFlightPlan = "C:/test2";
                    }

                    // If the user presses '3', it will exit the program.
                    else if (userInput == "3")
                    {
                        // Set tryAgain to false, so it will exit the while loop.
                        tryAgain = false;
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input, please try again.");
                        continue;
                    }

                    simConnect.FlightPlanLoad(targetedFlightPlan);

                } while (tryAgain);
            }
            catch (COMException ex)
            {
                Console.WriteLine("Unable to connect to SimConnect. Error Code: " + ex.ErrorCode);
                return;
            }

            if (simConnect != null)
            {
                simConnect.Dispose();
                simConnect = null;
                Console.WriteLine("Disconnected from SimConnect");
            }
        }

        static void SimConnect_RecvOpenEventHandler(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            Console.WriteLine("SimConnect_RecvOpenEventHandler");
        }
    }
}
