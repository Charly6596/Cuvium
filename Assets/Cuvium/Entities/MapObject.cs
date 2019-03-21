using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cuvium.Entities
{
    public abstract class MapObject : ScriptableObject
    {
        public Player Owner;
        public float Health;
        public string Name;
    }
}

