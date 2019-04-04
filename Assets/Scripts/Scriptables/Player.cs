using UnityEngine;
using System.Collections.Generic;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "Cuvium/Player")]
    public class Player : CuviumScriptable
    {
        public SelectedObjectCollection SelectedObjects;
        public Faction StartingFaction;
        public bool Human;
    }
}

