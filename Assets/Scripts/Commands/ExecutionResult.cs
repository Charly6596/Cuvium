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
            ErrorMessage = errorMessage;
            HasErrorMessage = true;
        }

        private ExecutionResult(Command command)
        {
            Command = command;
            ErrorMessage = string.Empty;
            HasErrorMessage = false;
        }
        public static ExecutionResult Suscess(Command command)
        {
            return new ExecutionResult(command);
        }
        public static ExecutionResult InvalidTarget(Command command)
        {
            return new ExecutionResult("Invalid Target" + command.Context.Target.Hit.transform.name, command);
        }

        public static ExecutionResult InvalidOperation(Command command)
        {
            return new ExecutionResult("Invalid Operation: " + command.Name, command);
        }

    }
}

