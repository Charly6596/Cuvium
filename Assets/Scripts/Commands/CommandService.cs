using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Cuvium.Attributes;

namespace Cuvium.Commands
{
    public class CommandService
    {
        public ReadOnlyCollection<ModuleInfo> Commands { get; private set; }

        public void RegisterModules(Assembly assembly)
        {
            var types = assembly.ExportedTypes;
            var commandModules = new List<ModuleInfo>();
            foreach(var type in types)
            {
                if(type.BaseType != typeof(CommandModule))
                {
                    continue;
                }
                var moduleInfo = new ModuleInfo();
                moduleInfo.Commands = GetCommands(type) as ReadOnlyCollection<CommandInfo>;
                moduleInfo.Attributes = type.GetCustomAttributes() as ReadOnlyCollection<Attribute>;
                moduleInfo.Name = type.Name;
                commandModules.Add(moduleInfo);
            }
            Commands = commandModules.AsReadOnly();
        }

        private IEnumerable<CommandInfo> GetCommands(Type module)
        {
            var commands = module
                .GetMethodsWithAttribute<CommandAttribute>();
            var commandInfos = new List<CommandInfo>();
            foreach(var command in commands)
            {
                if(command.MethodInfo.ReturnType != typeof(ExecutionResult))
                {
                    continue;
                }
                var cmd = new CommandInfo();
                var parameterInfos = new List<ParameterInfo>();
                var parameters = command.MethodInfo.GetParameters();
                foreach(var parameter in parameters)
                {
                    var p = new ParameterInfo();
                    p.IsOptional = parameter.IsOptional;
                    p.Name = parameter.Name;
                    p.Attributes = parameter.GetCustomAttributes() as ReadOnlyCollection<Attribute>;
                    p.Type = parameter.ParameterType;
                    p.DefaultValue = parameter.DefaultValue;
                    parameterInfos.Add(p);
                }
                cmd.Name = command.AttributeInfo.Name;
                cmd.Parameters = parameterInfos.AsReadOnly();
                cmd.Attributes = command.MethodInfo.GetCustomAttributes() as ReadOnlyCollection<Attribute>;
                commandInfos.Add(cmd);
            }
            return commandInfos;
        }
    }

    public class ModuleInfo
    {
        public ReadOnlyCollection<CommandInfo> Commands { get; internal set;}
        public ReadOnlyCollection<Attribute> Attributes { get; internal set; }
        public string Name { get; internal set; }

        internal ModuleInfo(){}
    }

    public class CommandInfo
    {
        public ReadOnlyCollection<ParameterInfo> Parameters { get; internal set; }
        public ReadOnlyCollection<Attribute> Attributes { get; internal set; }
        public string Name { get; internal set; }

        internal CommandInfo()
        {
        }
    }

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

