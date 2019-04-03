using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Cuvium.Commands
{
    public class CommandInfo
    {
        public ReadOnlyCollection<ParameterInfo> Parameters { get; internal set; }
        public ReadOnlyCollection<Attribute> Attributes { get; internal set; }
        public string Name { get; internal set; }
        public Type ControllerTarget { get; internal set; }
        public ModuleInfo Module { get; internal set; }
        public MethodInfo MethodInfo { get; internal set; }

        internal CommandInfo()
        {
        }
    }
}

