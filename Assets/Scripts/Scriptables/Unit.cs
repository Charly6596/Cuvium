using UnityEngine;
using Cuvium.Commands;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "Cuvium/Unit")]
    public class Unit : CuviumScriptable
    {
        public float Speed;
        public int Attack;
        public int Defense;
        public bool NeedsFood = true;
        public Sprite Icon;
        public Faction Faction;
        public Item[] Inventory;
    }
}

