namespace MarsRover
{
    /// <summary>
    /// A command that a rover can execute.
    /// </summary>
    public struct RoverCommand : ICommand
    {
        private string _command;


        public RoverCommand(string command)
        {
            _command = command;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}