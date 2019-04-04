using System;

namespace Cuvium.Commands
{
    public class TargetControllerAttribute : Attribute
    {
        public Type Target { get; internal set; }

        /// <summary>
        ///   Commands with this attribute will be restricted to be used only with the specified type of controller
        /// </summary>
        public TargetControllerAttribute(Type target)
        {
            Target = target;
        }
    }
}
