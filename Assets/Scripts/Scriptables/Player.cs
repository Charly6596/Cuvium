using UnityEngine;
using System.Collections.Generic;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "Player")]
    public class Player : CuviumModel
    {
        public List<Unit> Units;
    }
}

