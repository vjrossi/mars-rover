using System.Text.RegularExpressions;

namespace MarsRover.Core.Models;

/// <summary>
/// A parser for rover commands.
/// </summary>
public class RoverCommandParser
{
    /// <summary>
    /// Check if the command is an initialisation command.
    /// </summary>
    /// <param name="command">The command to check.</param>
    /// <returns>True if the command is an initialisation command, false otherwise.</returns>
    public bool IsInitialisation(string command)
    {
        return Regex.IsMatch(command, @"^\d+\s+\d+$");
    }

    /// <summary>
    /// Parse the commands for a rover.
    /// </summary>
    /// <param name="commandString">The command string.</param>
    /// <param name="rover">The rover.</param>
    /// <returns>The commands for the rover.</returns>
    /// <exception cref="ArgumentException">Thrown if the command string is not valid.</exception>
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


