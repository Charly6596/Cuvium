using UnityEngine;
using System.Collections.Generic;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "Faction")]
    public class Faction : CuviumModel
    {
        public List<Unit> Units;
    }
}

