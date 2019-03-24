using UnityEngine;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "Cuvium")]
    public abstract class CuviumModel : ScriptableObject
    {
        public string Name;
    }
}

