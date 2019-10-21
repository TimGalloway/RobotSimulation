using Alba.CsConsoleFormat;
using System;
using System.IO;

namespace RobotSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set the table size
            int maxX = 4;
            int maxY = 4;

            // Read a text file line by line.  
            string[] lines = File.ReadAllLines("C:\\Source\\RobotSimulation\\commands.txt");

            // Create RobotPosition object
            RobotPosition robotPosition = new RobotPosition();
            
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
                    bool boolRet = robotPosition.Place(Convert.ToInt32(arrArgs[0]), Convert.ToInt32(arrArgs[1]), arrArgs[2], maxX, maxY);
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
                    if (!robotPosition.Move(maxX, maxY))
                    {
                        Console.Write(" - Ignored");
                    }
                }
                if ((line == "REPORT") && robotPosition.Valid)
                {
                    ConsoleRenderer.RenderDocument(robotPosition.Report(maxX,maxY));
                }

                Console.WriteLine("");
            }
        }
    }
}
