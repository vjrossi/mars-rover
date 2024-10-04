using System.Text.RegularExpressions;

namespace MarsRover;

public class RoverCommandParser
{
    public bool IsInitialisation(string command)
    {
        return Regex.IsMatch(command, @"^\d+\s+\d+$");
    }

    public bool IsMove(string command)
    {
        return Regex.IsMatch(command, @"^[LRM]+$");
    }

    public List<ICommand> GetCommands(string commandString, IRover rover)
    {
        List<ICommand> commands = [];

        if (IsInitialisation(commandString))
        {
            throw new ArgumentException("Initialisation should already have been performed");
        }

        foreach (char command in commandString)
        {
            switch (command)
            {
                case 'L':
                    commands.Add(new RotateCommand(rover, TurnDirection.L));
                    break;
                case 'R':
                    commands.Add(new RotateCommand(rover, TurnDirection.R));
                    break;
                case 'M':
                    commands.Add(new MoveCommand(rover, 1));
                    break;
            }
        }

        return commands;
    }
}


