using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Cuvium.Attributes;

namespace Cuvium.Commands
{
    public class CommandService
    {
        public void RegisterModules(Assembly assembly)
        {
            var types = assembly.ExportedTypes;
            var commandModules = new List<Type>();
            foreach(var type in types)
            {
                if(type.BaseType == typeof(CommandModule))
                {
                    commandModules.Add(type);
                }
            }
        }

        private IEnumerable<CommandInfo> GetComamnds(Type module)
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
                    p.Attributes = parameter.GetCustomAttributes();
                    p.Type = parameter.ParameterType;
                    p.DefaultValue = parameter.DefaultValue;
                    parameters.Add(p);
                }
                cmd.Parameters = parameters;
                cmd.Name = command.AttributeInfo.Name;
                commandInfos.Add(cmd);
            }
            return commandInfos;
        }
    }

    public class ModuleInfo
    {
        public ReadOnlyCollection<CommandInfo> Commands { get; internal set;}

        internal ModuleInfo(IEnumerable<CommandInfo> commands)
        {
            Commands = commands;
        }
    }

    public class CommandInfo
    {
        public ReadOnlyCollection<ParameterInfo> Parameters { get; internal set; }
        public string Name { get; internal set; }

        internal CommandInfo(IEnumerable<ParameterInfo> parameters)
        {
            Attributes = attributes;
        }
    }

    public class ParameterInfo
    {
        public bool IsOptional { get; internal set; }
        public string Name { get; internal set; }
        public ReadOnlyCollection<Attribute> Attributes { get; internal set; }
        public Type Type { get; internal set; }
        public object DefaultValue { get; internal set; }
    }
}

