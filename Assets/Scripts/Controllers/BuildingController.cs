using UnityEngine;
using System.Threading.Tasks;
using Cuvium.Commands;

namespace Cuvium.Core
{
    public class BuildingController : CuviumController
    {
        public Player owner;
        public Unit unit;
        public UnitController prefab;

        public override void Command(ScriptableCommand command)
        {
            
        }

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

