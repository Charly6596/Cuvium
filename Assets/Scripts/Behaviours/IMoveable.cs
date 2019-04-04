using UnityEngine;

namespace Cuvium.Behaviours
{
    public interface IMoveable
    {
        int Speed { get; set; }
        void Move(Vector3 destination);
    }
}

