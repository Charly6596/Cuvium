using UnityEngine;
using System.Linq;

namespace Cuvium.InputManager
{
    [CreateAssetMenu(
                     fileName = "UnitCollection.asset",
                     menuName = "Cuvium/InputManager/Key Code Collection",
                     order = 120)]
    public class KeyCodeCollection : Collection<KeyCodeVariable>
    {
        private static KeyCodeCollection instance;
        public static KeyCodeCollection Instance
        {
            get
            {
                if(instance is null)
                {
                    instance = Resources.FindObjectsOfTypeAll<KeyCodeCollection>().FirstOrDefault();
                }
                return instance;
            }
        }

        void OnDisable()
        {
            Clear();
        }
    }
}

