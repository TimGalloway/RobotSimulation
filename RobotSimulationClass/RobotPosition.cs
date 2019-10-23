using Alba.CsConsoleFormat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RobotSimulationClass
{
    //RobotPosition class - this contains the current state of the robot
    public class RobotPosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public String Dir { get; set; }
        public bool Valid { get; set; }
        public RobotPosition()
        {
            this.Valid = false;
        }
        public bool Place(int x, int y, string Direction)
        {
            if ((x >= 0) && (x <= Desk.maxX) && (y >= 0) && (y <= Desk.maxY))
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

        public bool Move()
        {
            bool boolReturn = true;
            if (this.Dir == "NORTH")
            {
                this.Y++;
                if (this.Y > Desk.maxY)
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
                if (this.X > Desk.maxX)
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
        public List<String> commandList()
        {
            //Read the command text file into the list
            List<string> retList = File.ReadAllLines("C:\\Source\\RobotSimulation\\commands.txt").ToList();

            return retList;
        }
    }
}
