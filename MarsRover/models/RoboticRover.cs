
namespace MarsRover
{
    public interface IRover
    {
        Position RoverPosition { get; }
        Facing RoverFacing { get; }
        string ExecuteCommands(List<ICommand> commands);
        Position Move(int units);
        void Rotate(TurnDirection direction);
    }

    /// <summary>
    /// In real life this would be a dummy rover that is used to simulate the rover's path.
    /// </summary>
    public class DummyRover : RoboticRover
    {
        private RoverCamera _camera;

        public DummyRover(Position initialPosition, Facing initialFacing, ICamera camera) : base(initialPosition, initialFacing, camera)
        {
            _camera = new RoverCamera();
        }

        public string ExecuteCommand(ICommand command) 
        {
            command.Execute();
            return _camera.TakePhoto(RoverPosition.X, RoverPosition.Y, RoverFacing);
        }
    }

    /// <summary>
    /// A robotic rover that can move and rotate.   
    /// </summary>
    public class RoboticRover : IRover
    {
        public Position RoverPosition { get; private set; } = new Position(-1, -1);
        public Facing RoverFacing { get; private set; } = Facing.N;
        private RoverCamera _camera;
        private const int _maxSpeed = 1;
        private bool IsInitialised => RoverPosition.X != -1 && RoverPosition.Y != -1;

        public RoboticRover(Position position, Facing facing, ICamera camera)
        {
            RoverPosition = position;
            RoverFacing = facing;
            _camera = new RoverCamera();
        }

        /// <summary>
        /// Execute a list of commands.
        /// </summary>
        /// <param name="commands">The list of commands to execute.</param>
        public string ExecuteCommands(List<ICommand> commands)
        {
            foreach (ICommand command in commands)
            {
                command.Execute();
            }

            return _camera.TakePhoto(RoverPosition.X, RoverPosition.Y, RoverFacing);
        }

        /// <summary>
        /// Move the rover forward or backward.
        /// </summary>
        /// <param name="units">The number of units to move.</param>
        /// <returns>The new position of the rover.</returns>
        /// <exception cref="Exception">Thrown when the rover position or facing is not initialised.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the units are less than 0 or greater than the maximum speed.</exception>
        public Position Move(int units)
        {
            if (!IsInitialised)
            {
                throw new Exception("Rover position or facing not initialised");
            }

            if (units < 0 || units > _maxSpeed)
            {
                throw new ArgumentOutOfRangeException($"Units must be between 0 and {_maxSpeed}.");
            }

            int newX = RoverPosition.X;
            int newY = RoverPosition.Y;

            switch (RoverFacing)
            {
                case Facing.N:
                    newY += units;
                    break;
                case Facing.E:
                    newX += units;
                    break;
                case Facing.S:
                    newY -= units;
                    break;
                case Facing.W:
                    newX -= units;
                    break;
            }

            RoverPosition = new Position(newX, newY);
            return RoverPosition;
        }

        /// <summary>
        /// Rotate the rover left or right.
        /// </summary>
        /// <param name="direction">The direction to rotate.</param>
        /// <returns>True if the rover rotated, false otherwise.    </returns>
        public void Rotate(TurnDirection direction)
        {
            if (!IsInitialised)
            {
                throw new Exception("Rover position or facing not initialised");
            }

            RoverFacing = (Facing)((int)(RoverFacing + (direction == TurnDirection.L ? 3 : 1)) % 4);
        }
    }
}

