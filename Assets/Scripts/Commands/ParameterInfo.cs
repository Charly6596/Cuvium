using System;
using System.Collections.ObjectModel;

namespace Cuvium.Commands
{
    public class ParameterInfo
    {
        public bool IsOptional { get; internal set; }
        public string Name { get; internal set; }
        public ReadOnlyCollection<Attribute> Attributes { get; internal set; }
        public Type Type { get; internal set; }
        public object DefaultValue { get; internal set; }

        internal ParameterInfo() {}
    }
}

