using UnityEngine;

namespace Cuvium.Commands
{
    [CreateAssetMenu(menuName = ("Cuvium/Command Set"))]
    public class CommandSet : Collection<Command>
    {
        public bool Has(Command command)
        {
            return List.Contains(command);
        }

        public bool TryConvert(string text, out Command command)
        {
            var commands = List as Command[];
            for(var i = 0; i < commands.Length; i++)
            {
                var cmd = commands[0];
                if(cmd.Name == text)
                {
                    command = cmd;
                    return true;
                }
            }
            command = null;
            return false;
        }
    }
}

