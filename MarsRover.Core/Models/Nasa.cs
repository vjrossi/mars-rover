namespace MarsRover.Core.Models;

/// <summary>
/// This is the 'simulation' class. It is responsible for managing the rovers and the exploration zone.
/// </summary>
public class Nasa
{
    private ExplorationZone? _explorationZone;
    private RoverCommandParser _roverCommandParser;

    public Nasa()
    {
        _roverCommandParser = new RoverCommandParser();
    }

    /// <summary>
    /// Accepts a string of commands, either a 'rover initialise' command
    /// of the form '1 2 N' or a series of 'M', 'L', 'R' characters
    /// for moving the rover around the plateau.
    /// A rover initialisation command will be followed by a series of 
    /// movement commands.
    /// </summary>
    /// <param name="commandStrings">The command string to accept.</param>
    /// <returns>A list of intermediate positions and facings for each step of the rover's movement.</returns>
    public List<string> AcceptCommands(string[] commandStrings)
    {
        Console.WriteLine($"Received commands: {string.Join(", ", commandStrings)}");
        
        if (_roverCommandParser.IsInitialisation(commandStrings[0]))
        {
            _explorationZone = new ExplorationZone(int.Parse(commandStrings[0].Split(' ')[0]), int.Parse(commandStrings[0].Split(' ')[1]));
            return new List<string> { "Exploration zone set" };
        }

        if (_explorationZone == null)
        {
            throw new Exception("Exploration zone not set");
        }

        int x = int.Parse($"{commandStrings[0].Split(' ')[0]}");
        int y = int.Parse($"{commandStrings[0].Split(' ')[1]}");
        Facing facing = Enum.Parse<Facing>($"{commandStrings[0].Split(' ')[2]}");
        RoboticRover rover = new(new Position(x, y), facing, new RoverCamera());

        List<ICommand> commands = SimulateRoverPath(new Position(x, y), facing, commandStrings[1], rover);

        return ExecuteCommandsWithIntermediateSteps(rover, commands);
    }

    /// <summary>
    /// Simulate the rover path according to the commands, and if any command would send the rover off the grid, remove the command from the list
    /// </summary>
    private List<ICommand> SimulateRoverPath(Position initialPosition, Facing initialFacing, string commandsString, IRover rover)
    {
        DummyRover dummyRover = new(initialPosition, initialFacing, new RoverCamera());
        List<ICommand> dummyCommands = _roverCommandParser.GetCommands(commandsString, dummyRover);
        string newCommandsString = "";
        for (int i = 0; i < dummyCommands.Count; i++)
        {
            ICommand command = dummyCommands[i];
            dummyRover.ExecuteCommand(command);
            if (_explorationZone!.IsPositionValid(dummyRover.RoverPosition))
            {
                newCommandsString += commandsString[i];
            }
        }

        return _roverCommandParser.GetCommands(newCommandsString, rover);
    }

    /// <summary>
    /// Execute commands and return a list of intermediate steps.
    /// </summary>
    private List<string> ExecuteCommandsWithIntermediateSteps(RoboticRover rover, List<ICommand> commands)
    {
        Console.WriteLine($"Executing commands: {string.Join(", ", commands)}");
        List<string> intermediateSteps = [$"{rover.RoverPosition.X} {rover.RoverPosition.Y} {rover.RoverFacing}"];

        foreach (ICommand command in commands)
        {
            command.Execute();
            intermediateSteps.Add($"{rover.RoverPosition.X} {rover.RoverPosition.Y} {rover.RoverFacing}");
        }

        Console.WriteLine($"Intermediate steps: {string.Join(", ", intermediateSteps)}");
        return intermediateSteps;
    }
}