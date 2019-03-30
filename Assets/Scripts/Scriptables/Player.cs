using UnityEngine;
using System.Collections.Generic;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "Cuvium/Player")]
    public class Player : CuviumScriptable
    {
        public UnitCollection UnitCollection;
        public SelectedUnitCollection SelectedUnits;
        public List<Building> Buildings;
        public Faction StartingFaction;

        public void AddUnit(UnitController unit)
        {
            UnitCollection.Add(unit);
            Debug.Log("Added unit with " + unit.currentStats.Attack + " attack");
        }

        public void RemoveUnit(UnitController unit)
        {
            UnitCollection.Remove(unit);
            Debug.Log("Removed unit with " + unit.currentStats.Attack + " attack");
        }

        [Button]
        public void DisplayUnits()
        {
            foreach(var unit in UnitCollection)
            {
                Debug.Log($"Attack: {unit.currentStats.Attack}");
            }
        }
    }
}

