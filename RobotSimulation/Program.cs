using System;
using System.IO;

namespace RobotSimulation
{
    //RobotPosition class - this contains the current state of the robot
    class RobotPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public String Dir { get; set; }
        public bool Valid { get; set; }
        public RobotPosition()
        {
            this.Valid = false;
        }
    }
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
                    robotPosition.X = Convert.ToInt32(arrArgs[0]);
                    robotPosition.Y = Convert.ToInt32(arrArgs[1]);
                    robotPosition.Dir = arrArgs[2];
                    if ((robotPosition.X >= 0) && (robotPosition.X <= maxX) && (robotPosition.Y >= 0) && (robotPosition.Y <= maxY))
                    {
                        robotPosition.Valid = true;
                    }
                    else
                    {
                        Console.Write(" - Ignored");
                    }
                }
                if ((line == "LEFT") && robotPosition.Valid)
                {
                    string currentDir = robotPosition.Dir;
                    switch (currentDir)
                    {
                        case "NORTH":
                            robotPosition.Dir = "WEST";
                            break;
                        case "SOUTH":
                            robotPosition.Dir = "EAST";
                            break;
                        case "EAST":
                            robotPosition.Dir = "NORTH";
                            break;
                        case "WEST":
                            robotPosition.Dir = "SOUTH";
                            break;
                    }
                }
                if ((line == "RIGHT") && robotPosition.Valid)
                {
                    string currentDir = robotPosition.Dir;
                    switch (currentDir)
                    {
                        case "NORTH":
                            robotPosition.Dir = "EAST";
                            break;
                        case "SOUTH":
                            robotPosition.Dir = "WEST";
                            break;
                        case "EAST":
                            robotPosition.Dir = "SOUTH";
                            break;
                        case "WEST":
                            robotPosition.Dir = "NORTH";
                            break;
                    }
                }
                if ((line == "MOVE") && robotPosition.Valid)
                {
                    if (robotPosition.Dir == "NORTH")
                    {
                        robotPosition.Y++;
                        if (robotPosition.Y > maxY)
                        {
                            robotPosition.Y--;
                            Console.Write(" - Ignored");
                        }
                    }
                    if (robotPosition.Dir == "SOUTH")
                    {
                        robotPosition.Y--;
                        if (robotPosition.Y < 0)
                        {
                            robotPosition.Y++;
                            Console.Write(" - Ignored");
                        }
                    }
                    if (robotPosition.Dir == "EAST")
                    {
                        robotPosition.X++;
                        if (robotPosition.X > maxX)
                        {
                            robotPosition.X--;
                            Console.Write(" - Ignored");
                        }
                    }
                    if (robotPosition.Dir == "WEST")
                    {
                        robotPosition.X--;
                        if (robotPosition.X < 0)
                        {
                            robotPosition.X++;
                            Console.Write(" - Ignored");
                        }
                    }
                }
                if ((line == "REPORT") && robotPosition.Valid)
                {
                    Console.Write(" - Current Position: " + robotPosition.X + "," + robotPosition.Y + " Facing: " + robotPosition.Dir);
                }

                Console.WriteLine("");
            }
        }
    }
}
