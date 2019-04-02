using UnityEngine;

namespace Cuvium.InputManager
{
    [System.Serializable]
    public sealed class KeyCodeReference : BaseReference<KeyCode, KeyCodeVariable>
    {
        public KeyCodeReference() : base() { }
        public KeyCodeReference(KeyCode value) : base(value) { }
    }
}

