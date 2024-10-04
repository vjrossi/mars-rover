using MarsRover.Core.Models;

namespace MarsRover
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Check if the user has provided the correct number of arguments
            if (args.Length < 6)
            {
                Console.WriteLine("Usage: --grid x y --commands \"x y Direction COMMANDS\" \"x y Direction COMMANDS\" ... (e.g. --grid 50 50 --commands \"1 2 N LMLMLMLMM\" \"3 3 E MMRMMRMRRM\")");
                return;
            }

            try
            {
                int gridX = 0, gridY = 0;
                List<string> roverCommands = new List<string>();

                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "--grid":
                            gridX = int.Parse(args[++i]);
                            gridY = int.Parse(args[++i]);
                            break;
                        case "--commands":
                            while (++i < args.Length && !args[i].StartsWith("--"))
                            {
                                roverCommands.Add(args[i]);
                            }
                            i--; // Adjust index as we've looked one ahead
                            break;
                    }
                }

                if (gridX <= 0 || gridY <= 0)
                {
                    throw new ArgumentException("Grid dimensions must be positive integers.");
                }

                if (roverCommands.Count == 0)
                {
                    throw new ArgumentException("Commands cannot be empty.");
                }

                Nasa nasa = new();
                string gridSetup = nasa.AcceptCommands([$"{gridX} {gridY}"]);
                Console.WriteLine(gridSetup);

                foreach (var roverCommand in roverCommands)
                {
                    string[] parts = roverCommand.Split(new[] { ' ' }, 4, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 3)
                    {
                        throw new ArgumentException($"Invalid rover command format: {roverCommand}. Expected format: 'x y Direction [Commands]'");
                    }

                    string init = $"{parts[0]} {parts[1]} {parts[2]}";
                    string commands = parts.Length > 3 ? parts[3] : ""; // If no commands, use empty string

                    string result = nasa.AcceptCommands([init, commands]);
                    Console.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
