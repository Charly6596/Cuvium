using UnityEngine;
using System;

namespace Cuvium.Core
{
    public class BuildingController : CuviumController
    {
        public Unit unit;
        public UnitController prefab;

        public override void InitializeStats(CuviumScriptable scriptable)
        {
            throw new NotImplementedException();
        }

        public void CreateUnit()
        {
            var instance = Instantiate(prefab);
            instance.Owner = Owner;
            instance.BaseStats = unit;
            instance.transform.position = new Vector3(15, 0, 0);
            instance.Move(transform.position);
        }
    }
}

