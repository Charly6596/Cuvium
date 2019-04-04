namespace Cuvium.Commands
{
    public struct ExecutionResult
    {
        public string ErrorMessage { get; private set; }
        public bool HasErrorMessage { get; private set; }
        public CommandInfo Command { get; private set; }

        private ExecutionResult(string errorMessage, CommandInfo command)
        {
            Command = command;
            ErrorMessage = errorMessage;
            HasErrorMessage = true;
        }

        private ExecutionResult(CommandInfo command)
        {
            Command = command;
            ErrorMessage = string.Empty;
            HasErrorMessage = false;
        }
        public static ExecutionResult Suscess(CommandInfo command)
        {
            return new ExecutionResult(command);
        }
        public static ExecutionResult InvalidTarget(CommandInfo command)
        {
            return new ExecutionResult("Invalid Target" + command.Name, command);
        }

        public static ExecutionResult InvalidOperation(CommandInfo command)
        {
            return new ExecutionResult("Invalid Operation: " + command.Name, command);
        }

    }
}

