using Cuvium.Core;
using UnityEngine;

namespace Cuvium.Commands
{
    public class CommandContext
    {
        public Player Player { get; private set; }
        public Target Target { get; private set; }
        public CuviumController Controller { get; private set; }

        public CommandContext(Player player, CuviumController controller)
        {
            Player = player;
            Target = new Target();
            Controller = controller;
        }
    }
}

