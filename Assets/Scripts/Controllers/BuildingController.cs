using UnityEngine;
using System.Threading.Tasks;

namespace Cuvium.Core
{
    public class BuildingController : MonoBehaviour
    {
        public Player owner;
        public Unit unit;
        public UnitController prefab;

        public void CreateUnit()
        {
            var instance = Instantiate(prefab);
            instance.unit = unit;
            instance.Owner = owner;
            instance.transform.position = new Vector3(15, 0, 0);
            instance.Move(transform.position);
        }
    }
}

