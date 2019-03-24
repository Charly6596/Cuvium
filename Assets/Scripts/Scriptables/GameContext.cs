using UnityEngine;
using System.Collections.Generic;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "GameContext")]
    public class GameContext : CuviumModel
    {
        public List<Player> Players;
    }
}

