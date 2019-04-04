using System;

namespace Cuvium.Commands
{
    public class CommandAttribute : Attribute
    {
        public string Name { get; private set; }

        public CommandAttribute(string name)
        {
            Name = name;
        }
    }
}

