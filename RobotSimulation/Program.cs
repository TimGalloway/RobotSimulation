using Alba.CsConsoleFormat;
using System;
using System.IO;

namespace RobotSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read a text file line by line.  
            string[] lines = File.ReadAllLines("C:\\Source\\RobotSimulation\\commands.txt");

            // Create required objects
            RobotPosition robotPosition = new RobotPosition();

            //Set the table size
            Desk.maxX = 4;
            Desk.maxY = 4;
            
            foreach (string line in lines) { 
                Console.Write(line);

                //Should this command be ignored
                if ((!robotPosition.Valid) && (line.Length <= 6))
                {
                    Console.Write(" - Ignored");
                }

                //Process the commands one at a time
                if ((line.Length >= 6) && (line.Substring(0,5) == "PLACE"))
                {
                    string[] arrArgs = line.Substring(6).Split(",");
                    bool boolRet = robotPosition.Place(Convert.ToInt32(arrArgs[0]), Convert.ToInt32(arrArgs[1]), arrArgs[2]);
                    if (!boolRet)
                    {
                        Console.Write(" - Ignored");

                    }
                }
                if (((line == "LEFT") || (line == "RIGHT")) && robotPosition.Valid)
                {
                    robotPosition.Rotate(line);
                }
                if ((line == "MOVE") && robotPosition.Valid)
                {
                    if (!robotPosition.Move())
                    {
                        Console.Write(" - Ignored");
                    }
                }
                if ((line == "REPORT") && robotPosition.Valid)
                {
                    Console.WriteLine(""); 
                    ConsoleRenderer.RenderDocument(robotPosition.Report());
                }

                Console.WriteLine("");
            }
        }
    }
}
