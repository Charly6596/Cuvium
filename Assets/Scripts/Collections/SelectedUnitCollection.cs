using UnityEngine;

namespace Cuvium.Core
{
    [CreateAssetMenu(menuName = "Cuvium/Selected Unit Collection")]
    public class SelectedUnitCollection : Collection<UnitController>
    {
        private void OnEnable()
        {
            Clear();
        }

        private void OnDisable()
        {
            Clear();
        }

        public void Select(UnitController unit, bool multiSelect = false)
        {
            if(!multiSelect)
            {
                DeselectAll();
            }
            Add(unit);
            unit.Select();
        }

        public void Select(UnitController[] units, bool multiSelect = false)
        {
            if(!multiSelect)
            {
                DeselectAll();
            }
            foreach(var unit in units)
            {
                Select(unit, true);
            }
        }

        public void DeselectAll()
        {
            foreach(var unit in List)
            {
                ((UnitController)unit).Deselect();
            }
            Clear();
        }

        public void MoveAll(Vector3 destination)
        {
            var middle = GetMiddlePoint();
            foreach(var unit in List)
            {
                var u = (UnitController) unit;
                var offset = u.transform.position - middle;
                u.Move(destination + offset);
            }
        }

        private Vector3 GetMiddlePoint()
        {
            Vector3 posSum = Vector3.zero;
            foreach(var unit in List)
            {
                var u = (UnitController) unit;
                posSum += u.transform.position;
            }
            Vector3 middle = posSum / List.Count;
            return middle;
        }

        public void Attack(UnitController target)
        {
            foreach(var unit in List)
            {
                var u = (UnitController) unit;
                u.Attack(target);
            }
        }
    }
}

