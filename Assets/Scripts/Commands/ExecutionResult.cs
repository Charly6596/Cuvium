namespace Cuvium.Commands
{
    public struct ExecutionResult
    {
        public string ErrorMessage { get; private set; }
        public bool HasErrorMessage { get; private set; }
        public Command Command { get; private set; }

        private ExecutionResult(string errorMessage, Command command)
        {
            Command = command;
            ErrorMesssage = errorMessage;
            HasErrorMessage = true;
        }

        private ExecutionResult(Command command)
        {
            Command = command;
            HasErrorMessage = false;
        }
        public static ExecutionResult Suscess(Command command)
        {
            return new ExeutionResult(command);
        }
        public static ExecutionResult InvalidTarget(Command command)
        {
            return new ExecutionResult("Invalid Target" + command.Context.Target.Hit.Name, command);
        }

        public static ExecutionResult InvalidOperation(Command command)
        {
            return new ExeutionResult("Invalid Operation: " + command.Name, command);
        }

    }
}

