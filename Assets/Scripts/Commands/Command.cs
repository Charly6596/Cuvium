using Cuvium.Core;

namespace Cuvium.Commands
{
    public class Command
    {
        public CommandContext Context { get; private set; }

        public Command(CommandContext context)
        {
            Context = context;
        }
    }
}

