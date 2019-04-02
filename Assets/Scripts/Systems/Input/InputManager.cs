using UnityEngine;

namespace Cuvium.InputManager
{
    public class InputManager : MonoBehaviour
    {
        public KeyCodeCollection Keys;
        public KeyCodeGameEvent OnKeyDown;
        public KeyCodeGameEvent OnKeyUp;
        public KeyCodeGameEvent OnKeyHold;

        private void Update()
        {
            foreach(var key in Keys)
            {
                if(Input.GetKeyUp(key))
                {
                    OnKeyUp.Raise(key);
                }
                else if(Input.GetKeyDown(key))
                {
                    OnKeyDown.Raise(key);
                }
                else if(Input.GetKey(key))
                {
                    OnKeyHold.Raise(key);
                }
            }
        }
    }
}

