namespace MarsRover;

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
    /// A rover will complete its sequence of commands by taking a photo,
    /// which it will upload to the NASA servers.
    /// </summary>
    /// <param name="commandStrings">The command string to accept.</param>
    /// <returns></returns>
    public string AcceptCommands(string[] commandStrings)
    {
        if (_roverCommandParser.IsInitialisation(commandStrings[0]))
        {
            _explorationZone = new ExplorationZone(int.Parse(commandStrings[0].Split(' ')[0]), int.Parse(commandStrings[0].Split(' ')[1]));
            return string.Empty;
        }

        if (_explorationZone == null)
        {
            throw new Exception("Exploration zone not set");
        }

        int x = int.Parse($"{commandStrings[0].Split(' ')[0]}");
        int y = int.Parse($"{commandStrings[0].Split(' ')[1]}");
        Facing facing = Enum.Parse<Facing>($"{commandStrings[0].Split(' ')[2]}");
        RoboticRover rover = new(new Position(x, y), facing, new RoverCamera());

        // first simulate the entire rover path according to the commands, and if 
        // any command would send the rover off the grid, remove the command from the list
        List<ICommand> commands = SimulateRoverPath( new Position(x, y), facing, commandStrings[1], rover);

        var result = rover.ExecuteCommands(commands);
        return result;
    }

    /// <summary>
    /// Simulate the rover path according to the commands, and if any command would send the rover off the grid, remove the command from the list
    /// </summary>
    /// <param name="initialPosition">The initial position of the rover.</param>
    /// <param name="initialFacing">The initial facing of the rover.</param>
    /// <param name="commandsString">The commands to simulate.</param>
    /// <param name="rover">The rover to simulate.</param>
    /// <returns>The list of commands that were successfully simulated.</returns>
    private List<ICommand> SimulateRoverPath(Position initialPosition, Facing initialFacing, string commandsString, IRover rover)
    {
        // use a dummy rover to simulate the path
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
}
