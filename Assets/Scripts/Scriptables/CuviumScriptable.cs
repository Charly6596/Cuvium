using UnityEngine;

namespace Cuvium.Core
{
    public abstract class CuviumScriptable : ScriptableObject
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        public CuviumController Controller;
    }
}

