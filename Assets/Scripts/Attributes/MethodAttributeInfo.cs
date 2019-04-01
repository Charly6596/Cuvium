using System;
using System.Reflection;

namespace Cuvium.Attributes
{
    public class MethodAttributeInfo<T> where T : Attribute
    {
        public MethodInfo MethodInfo { get; set; }
        public T AttributeInfo { get; set; }

        public MethodAttributeInfo(MethodInfo mInfo, T aInfo)
        {
            MethodInfo = mInfo;
            AttributeInfo = aInfo;
        }
    }
}

