// this class represents the exploration zone. it can be square or rectangular

namespace MarsRover;

public class ExplorationZone
{
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

    public int Width { get; set; }
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

