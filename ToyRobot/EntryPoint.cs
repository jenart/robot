using System;
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


            // create a robot


            // execute commands


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
    }
}
