using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cuvium.UI
{
    public class IconController : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private Text label;

        public void SetText(string text)
        {
            label.text = text;
        }

        public void SetSprite(Sprite sprite)
        {
            image.overrideSprite = sprite;
        }

        public void ResetValues()
        {
            image.overrideSprite = default(Sprite);
            label.text = default(string);
        }
    }
}

