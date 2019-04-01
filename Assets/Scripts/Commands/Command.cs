using Cuvium.Core;
using UnityEngine;

namespace Cuvium.Commands
{
    public abstract class Command : ScriptableObject
    {
        public string Name { get; protected set; }
        public CommandContext Context;

        public abstract ExecutionResult Execute(CommandContext context);
    }
}

