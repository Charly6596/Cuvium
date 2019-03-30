using UnityEngine;
using System.Collections.Generic;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "Cuvium/Faction")]
    public class Faction : CuviumScriptable
    {
        public List<Unit> Units;
        public string Name;
    }
}

