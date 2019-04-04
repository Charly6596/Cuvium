using UnityEngine;

namespace Cuvium.InputManager
{
    [CreateAssetMenu(menuName = "Cuvium/InputManager/Key Code variable")]
    public class KeyCodeVariable : BaseVariable<KeyCode>
    {
        void OnEnable()
        {
            if(!KeyCodeCollection.Instance.Contains(this))
            {
                KeyCodeCollection.Instance.Add(this);
            }
        }
    }
}

