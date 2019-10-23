using Alba.CsConsoleFormat;
using RobotSimulationClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotSimulation
{
    class RobotPosition : RobotSimulationClass.RobotPosition
    {
        public Document Report()
        {
            var headerThickness = new LineThickness(LineWidth.Double, LineWidth.Single);
            char arrow = ' ';
            List<Cell> children = new List<Cell>();
            for (int aa = Desk.maxX; aa >= 0; aa--)
            {
                for (int bb = 0; bb <= Desk.maxY; bb++)
                {
                    if ((this.Y == aa) && (this.X == bb))
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
            var doc = new Document(
                new Span("Current Position: " + this.X + "," + this.Y + " Facing: " + this.Dir) { Color = ConsoleColor.Yellow }, "", "\n",
                new Grid
                {
                    Color = ConsoleColor.Gray,
                    Columns = { GridLength.Auto, GridLength.Auto, GridLength.Auto, GridLength.Auto, GridLength.Auto },
                    Children = {
                        children
                    }
                }
            );
            return doc;
        }

    }
}
