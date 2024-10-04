namespace MarsRover
{
    public interface ICamera
    {
        string TakePhoto(int x, int y, Facing roverFacing);
    }

    public class RoverCamera : ICamera
    {
        public string TakePhoto(int x, int y, Facing roverFacing)
        {
            return $"{x} {y} {roverFacing}";
        }
    }
}