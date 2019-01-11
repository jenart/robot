using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyRobot
{
    public class Robot
    {
        public Position CurrentLocation { get; set; }
        public Heading CurrentHeading { get; set; }

        private IEnumerable<Command> _cmds;
        private bool _startingPlaceCmdReceived = false;


        public Robot()
        {
            _cmds = Enumerable.Empty<Command>();
            CurrentLocation = new Position(0, 0);
            CurrentHeading = Heading.IGNORE;
        }

        public void Init(IEnumerable<Command> commands)
        {
            _cmds = commands;
        }

        public void Execute()
        {
            foreach (var cmd in _cmds)
            {
                if (cmd.Operation != OpCode.PLACE && !_startingPlaceCmdReceived)
                {
                    continue; // ignore all commands until valid PLACE is received!
                }

                switch (cmd.Operation)
                {
                    case OpCode.PLACE:
                        if (PlaceLocationIsValid(cmd.Location))
                        {
                            CurrentLocation = cmd.Location;
                            CurrentHeading = cmd.Facing;
                            _startingPlaceCmdReceived = true; // we can begin processing other subsequent commands
                        }
                        else
                        {
                            ; // ignore the command
                        }
                        break;
                    case OpCode.LEFT:
                        // rotate 90 degrees counter-clockwise
                        CurrentHeading = CurrentHeading == Heading.NORTH ? Heading.WEST : (CurrentHeading - 1);
                        break;
                    case OpCode.RIGHT:
                        // rotate 90 degrees clockwise
                        CurrentHeading = CurrentHeading == Heading.WEST ? Heading.NORTH : (CurrentHeading + 1);
                        break;
                    case OpCode.MOVE:
                        MoveRobot();
                        break;
                    case OpCode.REPORT:
                        Report();
                        break;
                    default:
                        throw new Exception($"Invalid operation! [{cmd.Operation}]");
                }
            }
        }

        private void MoveRobot()
        {
            var newX = CurrentLocation.X;
            var newY = CurrentLocation.Y;

            switch (CurrentHeading)
            {
                case Heading.NORTH:
                    newY++;
                    break;
                case Heading.EAST:
                    newX++;
                    break;
                case Heading.SOUTH:
                    newY--;
                    break;
                case Heading.WEST:
                    newX--;
                    break;
                default:
                    throw new Exception($"Invalid current heading! [{CurrentHeading}]");
            }

            if (newX > 4 || newX < 0 || newY > 4 || newY < 0)
            {
                // invalid move... robot would fall off the table
                return;
            }

            // save new position
            CurrentLocation = new Position(newX, newY);
        }

        private void Report()
        {
            Console.WriteLine($"{CurrentLocation.X},{CurrentLocation.Y},{CurrentHeading}");
        }

        private bool PlaceLocationIsValid(Position pos)
        {
            return pos.X > -1 && pos.X < 5 && pos.Y > -1 && pos.Y < 5;
        }
    }
}
