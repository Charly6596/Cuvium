using Cuvium.Core;
using UnityEngine;

namespace Cuvium.Commands
{
    public abstract class Command : ScriptableObject
    {
        public CommandContext Context { get; private set; }
        public string Name { get; protected set; }

        public virtual ExecutionResult Execute(CommandContext context)
        {
            Context = context;
        }
    }
}

