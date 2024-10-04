namespace MarsRover.Core.Models;

/// <summary>
/// Interface for a camera.
/// </summary>
public interface ICamera
{
    string TakePhoto(int x, int y, Facing roverFacing);
}

/// <summary>
/// A camera that 'takes photos' of the rover's position and facing.
/// </summary>
public class RoverCamera : ICamera
{
    public string TakePhoto(int x, int y, Facing roverFacing)
    {
        return $"{x} {y} {roverFacing}";
    }
}