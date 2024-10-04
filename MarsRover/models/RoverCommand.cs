namespace MarsRover
{
    public struct RoverCommand : ICommand
    {
        private string _command;


        public RoverCommand(string command)
        {
            _command = command;
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return _command;
        }
    }
}