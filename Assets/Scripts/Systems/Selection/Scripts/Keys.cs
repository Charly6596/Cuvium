using UnityEngine;
using System.Collections.Generic;

namespace Cuvium.InputManager
{
    [CreateAssetMenu(menuName = "Cuvium/InputManager/KeyMap")]
    public class KeyMap : ScriptableObject
    {
        public KeyCode Command = KeyCode.Semicolon;
        public KeyCode LeftClick = KeyCode.Mouse0;
        public KeyCode RightClick = KeyCode.Mouse1;
        public KeyCode Cancel = KeyCode.Escape;

        public List<KeyCode> GetMappedKeys()
        {
            var keys = new List<KeyCode>();
            return keys;
        }
    }
}

