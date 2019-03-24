using UnityEngine;
using System.Collections.Generic;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "Cuvium/Faction")]
    public class Faction : CuviumModel
    {
        public List<Unit> Units;
    }
}

