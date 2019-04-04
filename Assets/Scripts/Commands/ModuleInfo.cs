using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cuvium.Commands
{
    public class ModuleInfo
    {
        public IEnumerable<CommandInfo> Commands { get; internal set;}
        public ReadOnlyCollection<Attribute> Attributes { get; internal set; }
        public string Name { get; internal set; }
        public Type ControllerTarget { get; internal set; }
        public Type Type { get; internal set; }

        internal ModuleInfo(){}
    }
}

