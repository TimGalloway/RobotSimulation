﻿using Alba.CsConsoleFormat;
using System;
using System.Collections.Generic;

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

        public Document Report()
        {
            var headerThickness = new LineThickness(LineWidth.Double, LineWidth.Single);
            char arrow = ' ';
            List<Cell> children = new List<Cell>();
            for (int xLoop = Desk.maxX; xLoop >= 0; xLoop--)
            {
                for (int yLoop = 0; yLoop <= Desk.maxY; yLoop++)
                {
                    if ((this.Y == xLoop) && (this.X == yLoop))
                    {
                        switch (this.Dir)
                        {
                            case "NORTH":
                                arrow = '^';
                                break;
                            case "SOUTH":
                                arrow = 'v';
                                break;
                            case "EAST":
                                arrow = '>';
                                break;
                            case "WEST":
                                arrow = '<';
                                break;
                        }
                        Cell cellA = new Cell(" " + arrow + " ") { Stroke = headerThickness };
                        children.Add(cellA);
                    }
                    else
                    {
                        Cell cellA = new Cell("   ") { Stroke = headerThickness };
                        children.Add(cellA);
                    }

                }
            }
            Grid grid = new Grid
            {
                Color = ConsoleColor.Gray
            };
            grid.Children.Add(children);
            for (int colCount = 0; colCount <= Desk.maxY; colCount++)
            {
                grid.Columns.Add(GridLength.Auto);
            }

            Document doc = new Document(
                new Span("Current Position: " + this.X + "," + this.Y + " Facing: " + this.Dir) { Color = ConsoleColor.Yellow }, "", "\n",
                grid
            );
            return doc;
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
    }
}
