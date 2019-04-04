using UnityEngine;
using System.Threading.Tasks;
using Cuvium.Commands;

namespace Cuvium.Core
{
    public class BuildingController : CuviumController
    {
        public Unit unit;
        public UnitController prefab;

        public void CreateUnit()
        {
            var instance = Instantiate(prefab);
            instance.unit = unit;
            instance.Owner = Owner;
            instance.transform.position = new Vector3(15, 0, 0);
            instance.Move(transform.position);
        }
    }
}

