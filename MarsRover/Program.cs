namespace MarsRover
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 6)
            {
                Console.WriteLine("Usage: --grid x y --commands xyf COMMANDS (e.g. --grid 5 5 --commands 1 2 N LMLMLMLMM)");
                return;
            }

            try
            {
                int gridX = 0, gridY = 0;
                string init = "";
                string commands = "";

                for (int i = 0; i < args.Length; i++)
                {
                    switch (args[i])
                    {
                        case "--grid":
                            gridX = int.Parse(args[++i]);
                            gridY = int.Parse(args[++i]);
                            break;
                        case "--commands":
                            init = args[++i];
                            commands = args[++i];
                            break;
                    }
                }

                if (gridX <= 0 || gridY <= 0)
                {
                    throw new ArgumentException("Grid dimensions and rover count must be positive integers.");
                }

                if (init.Length < 3 || commands.Length < 1)
                {
                    throw new ArgumentException("Invalid command format.");
                }

                Nasa nasa = new();
                string result = nasa.AcceptCommands([$"{gridX} {gridY}"]);
                result = nasa.AcceptCommands([$"{init[0]} {init[1]} {init[2]}", commands]);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
