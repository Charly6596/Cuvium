using UnityEngine;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "Cuvium/Unit")]
    public class Unit : CuviumModel
    {
        public float Speed;
        public int Attack;
        public bool NeedsFood = true;
        public Texture2D Icon;
        public Faction Faction;
        public Item[] Inventory;
    }
}

