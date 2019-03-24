using UnityEngine;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "Cuvium/Item")]
    public class Item : CuviumModel
    {
        public string Description;
        public Texture2D Icon;
        public AudioClip UsageSound;
    }
}

