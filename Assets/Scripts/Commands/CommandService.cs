using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Cuvium.Attributes;

namespace Cuvium.Commands
{
    public class CommandService
    {
        public IEnumerable<ModuleInfo> ModulesInfo { get; private set; }
        public IEnumerable<CommandInfo> Commands => ModulesInfo.SelectMany(m => m.Commands);
        private Dictionary<string, object> CommandsModules;
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
                moduleInfo.Name = type.Name;
                moduleInfo.Type = type;
                moduleInfo.Attributes = type.GetCustomAttributes() as ReadOnlyCollection<Attribute>;
                moduleInfo.Commands = GetCommands(moduleInfo);
                commandModules.Add(moduleInfo);
            }
            ModulesInfo = commandModules.AsReadOnly();
            InstantiateModules();
        }

        private void InstantiateModules()
        {
            CommandsModules = new Dictionary<string, object>();
            foreach(var module in ModulesInfo)
            {
                var obj = Activator.CreateInstance(module.Type);
                CommandsModules[module.Name] = obj;
            }
        }
        public ExecutionResult ExecuteCommand(CommandContext context)
        {
            var command = Search(context);
            if(command is null)
            {
                return ExecutionResult.Suscess(command);
            }
            var result = CommandsModules.TryGetValue(command.Module.Name, out var module);
            if(!result)
            {
                return ExecutionResult.InvalidTarget(command);
            }
            command.MethodInfo.Invoke(module, null);
            return ExecutionResult.Suscess(command);
        }

        public CommandInfo Search(CommandContext context)
        {
            return Search(context.Command);
        }

        public CommandInfo Search(string term)
        {
            var commands = GetAllCommands();
            foreach(var command in commands)
            {
                if(command.Name == term)
                {
                    return command;
                }
            }
            return null;
        }

        private IEnumerable<CommandInfo> GetAllCommands()
        {
            var commands = new List<CommandInfo>();
            foreach(var module in ModulesInfo)
            {
                if(module.Commands is null)
                {
                    continue;
                }
                foreach(var command in module.Commands)
                {
                    commands.Add(command);
                }
            }
            return commands;
        }

        private IEnumerable<CommandInfo> GetCommands(ModuleInfo module)
        {
            var commands = module
                .Type
                .GetMethodsWithAttribute<CommandAttribute>();
            var commandInfos = new List<CommandInfo>();
            foreach(var command in commands)
            {
                if(command.MethodInfo.ReturnType != typeof(ExecutionResult))
                {
                    continue;
                }
                var cmd = new CommandInfo();
                cmd.Module = module;
                cmd.Name = command.AttributeInfo.Name;
                cmd.Parameters = GetParameters(command.MethodInfo) as ReadOnlyCollection<ParameterInfo>;
                cmd.Attributes = command.MethodInfo.GetCustomAttributes() as ReadOnlyCollection<Attribute>;
                cmd.MethodInfo = command.MethodInfo;
                commandInfos.Add(cmd);
            }
            return commandInfos;
        }

        private IEnumerable<ParameterInfo> GetParameters(MethodInfo command)
        {
            var parameterInfos = new List<ParameterInfo>();
            var parameters = command.GetParameters();
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
            return parameterInfos;
        }
    }

    public class ModuleInfo
    {
        public IEnumerable<CommandInfo> Commands { get; internal set;}
        public ReadOnlyCollection<Attribute> Attributes { get; internal set; }
        public string Name { get; internal set; }
        public Type Type { get; internal set; }

        internal ModuleInfo(){}
    }

    public class CommandInfo
    {
        public ReadOnlyCollection<ParameterInfo> Parameters { get; internal set; }
        public ReadOnlyCollection<Attribute> Attributes { get; internal set; }
        public string Name { get; internal set; }
        public ModuleInfo Module { get; internal set; }
        public MethodInfo MethodInfo { get; internal set; }

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

