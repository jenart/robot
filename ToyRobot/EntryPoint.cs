using System;
using System.Collections.Generic;
using System.IO;

namespace ToyRobot
{
    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            // Process args
            if (!ArgsValid(args))
            {
                return;
            }

            // parse commands
            var commands = LoadCommands(args[1]);
            PrintCommands(commands);
            
            // create & initialise a new robot
            var robot = new Robot();
            robot.Init(commands);

            // execute commands
            robot.Execute();

            Console.ReadLine();
        }



        private static IEnumerable<Command> LoadCommands(string cmdFile)
        {
            // already checked for file existence
            var commands = new List<Command>();
            using (var reader = new StreamReader(cmdFile, true))
            {
                string cmdStr;
                while ((cmdStr = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(cmdStr))
                    {
                        continue; // ignore and keep reading file
                    }

                    // Handle PLACE command special case
                    if (cmdStr.StartsWith(OpCode.PLACE.ToString()))
                    {
                        var cmdToken = cmdStr.Split(' ');
                        var placeCmdTokens = cmdToken[1].Split(',');

                        // NOTE: could do more error handling/checking here in a real system
                        Enum.TryParse(cmdToken[0].Trim(), true, out OpCode cmd);
                        var position = new Position(int.Parse(placeCmdTokens[0]), 
                                                    int.Parse(placeCmdTokens[1]));
                        Enum.TryParse(placeCmdTokens[2].Trim(), true, out Heading heading);
                        
                        commands.Add(new Command(cmd, position, heading));
                    }
                    else
                    {
                        // all other commands...
                        Enum.TryParse(cmdStr.Trim(), true, out OpCode cmd);
                        commands.Add(new Command(cmd));
                    }
                }
            }

            return commands;
        }

        private static bool ArgsValid(string[] args)
        {
            if (args.Length != 2)
            {
                PrintUsage("Missing command arguments");
                return false;
            }

            if (!args[0].Equals("-f", StringComparison.InvariantCultureIgnoreCase))
            {
                PrintUsage("Invalid flag parameter!");
                return false;
            }

            if (string.IsNullOrWhiteSpace(args[1]) || !File.Exists(args[1]))
            {
                PrintUsage("Invalid command file specified or file does not exist!");
                return false;
            }

            return true;
        }

        private static void PrintUsage(string msg = null)
        {
            if (!string.IsNullOrWhiteSpace(msg))
            {
                Console.WriteLine();
                Console.WriteLine($"Error: {msg}");
            }

            Console.WriteLine();
            Console.WriteLine("Usage: ToyRobot -f filename");
            Console.WriteLine();
            Console.WriteLine("eg: ToyRobot -f \"c:\\dev\\robot1.txt\"");
            Console.WriteLine();
        }

        private static void PrintCommands(IEnumerable<Command> commands)
        {
            foreach (var cmd in commands)
            {
                Console.WriteLine(cmd);
            }
        }

    }
}
