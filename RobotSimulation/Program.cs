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
        public bool Place(int x, int y, string Direction, int maxX, int maxY)
        {
            if ((x >= 0) && (x <= maxX) && (y >= 0) && (y <= maxY))
            {
                this.X = x;
                this.Y = y;
                this.Dir = Direction;
                this.Valid = true;
            }
            else
            {
                this.Valid = false;
            }
            return this.Valid;
        }

        public bool Rotate(string Direction)
        {
            switch (Direction)
            {
                case "LEFT":
                    switch (this.Dir)
                    {
                    case "NORTH":
                        this.Dir = "WEST";
                        break;
                    case "SOUTH":
                            this.Dir = "EAST";
                        break;
                    case "EAST":
                            this.Dir = "NORTH";
                        break;
                    case "WEST":
                            this.Dir = "SOUTH";
                        break;
                    }
                    break;
                case "RIGHT":
                    switch (this.Dir)
                    {
                        case "NORTH":
                            this.Dir = "EAST";
                            break;
                        case "SOUTH":
                            this.Dir = "WEST";
                            break;
                        case "EAST":
                            this.Dir = "SOUTH";
                            break;
                        case "WEST":
                            this.Dir = "NORTH";
                            break;
                    }
                    break;
            }
            return true;
        }
        public string Report()
        {
            string strReturn = " - Current Position: " + this.X + "," + this.Y + " Facing: " + this.Dir;
            return strReturn;
        }

        public bool Move(int maxX, int maxY)
        {
            bool boolReturn = true;
            if (this.Dir == "NORTH")
            {
                this.Y++;
                if (this.Y > maxY)
                {
                    this.Y--;
                    boolReturn = false;
                }
            }
            if (this.Dir == "SOUTH")
            {
                this.Y--;
                if (this.Y < 0)
                {
                    this.Y++;
                    boolReturn = false;
                }
            }
            if (this.Dir == "EAST")
            {
                this.X++;
                if (this.X > maxX)
                {
                    this.X--;
                    boolReturn = false;
                }
            }
            if (this.Dir == "WEST")
            {
                this.X--;
                if (this.X < 0)
                {
                    this.X++;
                    boolReturn = false;
                }
            }
            return boolReturn;
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
                    Console.Write(robotPosition.Report());
                }

                Console.WriteLine("");
            }
        }
    }
}
