using UnityEngine;
using Cuvium.Core;

namespace Cuvium.Commands
{
    public abstract class ScriptableCommand : ScriptableObject
    {
        public string Name;

        public abstract void Execute(CommandContext context, CuviumController controller);
    }
}

