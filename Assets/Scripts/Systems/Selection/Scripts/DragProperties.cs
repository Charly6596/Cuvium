using UnityEngine;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "Cuvium/Properties/MultiSelectionProperties")]
    public class DragProperties : ScriptableObject
    {
        public Color BorderColor = Color.blue;
        public float BorderWidth;
        public Color SelectionColor = new Color(0.8f, 0.8f, 0.95f, 0.1f);

        void OnValidate()
        {
            if(BorderWidth < 0)
            {
                BorderWidth = 0;
            }
        }
    }
}

