using Cuvium.Core;
using UnityEngine;

namespace Cuvium.Commands
{
    public class CommandContext
    {
        public Player Player { get; private set; }
        public Target Target { get; private set; }

        public CommandContext(Player player)
        {
            Player = player;
            Target = new Target();
        }
    }
}

