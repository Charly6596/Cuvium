using System;

namespace Cuvium.Commands
{
    public class CommanAttribute : Attribute
    {
        public string Name { get; private set; };

        public CommandAttribute(string name)
        {
            Name = name;
        }
    }
}

