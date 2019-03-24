using UnityEngine;
using System.Collections.Generic;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "Cuvium/Player")]
    public class Player : CuviumModel
    {
        public List<Unit> Units;
        public List<Building> Buildings;
        public Faction StartingFaction;
    }
}

