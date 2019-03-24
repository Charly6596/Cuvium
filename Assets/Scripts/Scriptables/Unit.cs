using UnityEngine;

namespace Cuvium.Core
{
    public class Unit : CuviumModel
    {
        public float Speed;
        public int Attack;
        public bool NeedsFood = true;
        public Texture2D Icon;
        public Item[] Inventory;
    }
}

