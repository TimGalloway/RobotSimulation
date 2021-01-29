using Alba.CsConsoleFormat;
using System;
using System.IO;
using System.Configuration;

namespace RobotSimulation
{
    class Program
    {
        static void Main()
        {
            // Get settings from App.config file
            string commandFile;
            int deskSizeX;
            int deskSizeY;

            commandFile = ConfigurationManager.AppSettings.Get("commandFile");
            //Get the desk size and decrement by 1 to compensate for 0 based numbering
            deskSizeX = Int32.Parse(ConfigurationManager.AppSettings.Get("deskSizeX")) -1;
            deskSizeY = Int32.Parse(ConfigurationManager.AppSettings.Get("deskSizeY")) -1;

            // Read a text file line by line.  
            string[] lines = File.ReadAllLines(commandFile);

            // Create robot object
            RobotPosition robotPosition = new RobotPosition();

            //Set the table size
            Desk.maxX = deskSizeX;
            Desk.maxY = deskSizeY;
            
            //Read thru the command file one line at a time
            foreach (string line in lines) { 
                //Display the command
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
