using System;
using UnityEngine;

namespace Cuvium.Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : PropertyAttribute
    {
        private string _label;
        public string Label 
        {
            get
            {
                return _label;
            }
            set
            {
                _label = value;
                IsLabelDefined = true;
            }
        }
        public bool IsLabelDefined;

        public ButtonAttribute()
        {
        }
    }
}

