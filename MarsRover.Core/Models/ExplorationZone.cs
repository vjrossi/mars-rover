namespace MarsRover.Core.Models;

/// <summary>
/// Represents the exploration zone the rovers are navigating.
/// </summary>
public class ExplorationZone
{
    /// <summary>
    /// Constructor for the exploration zone.
    /// </summary>
    /// <param name="width">The width of the exploration zone.</param>
    /// <param name="height">The height of the exploration zone.</param>
    public ExplorationZone(int width, int height)
    {
        if (width <= 0 || height <= 0)
        {
            throw new ArgumentOutOfRangeException("Width and height must be positive integers.");
        }

        if (width * height < 1)
        {
            throw new ArgumentOutOfRangeException("Exploration zone must be at least 1x1.");
        }

        Width = width;
        Height = height;
    }

    /// <summary>
    /// The width of the exploration zone.
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// The height of the exploration zone.
    /// </summary>
    /// 
    public int Height { get; set; }

    /// <summary>
    /// Checks if a given position is within the exploration zone.
    /// </summary>
    /// <param name="position">The position to check.</param>
    /// <returns>True if the position is within the exploration zone, false otherwise.</returns>
    public bool IsPositionValid(Position position)
    {
        return position.X >= 0 && position.X < Width && position.Y >= 0 && position.Y < Height;
    }
}

