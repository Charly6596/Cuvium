using UnityEngine;
using Cuvium.Core;
using System.Linq;
using System.Collections.Generic;

namespace Cuvium.UI
{
    public class SelectedUnitController : MonoBehaviour
    {
        [SerializeField]
        private SelectedUnitCollection selectedUnits;
        [SerializeField]
        private Sprite crowdIcon;
        private Dictionary<Unit, int> UnitGroups;
        [SerializeField]
        private IconController selectedIcon;
        public GameObject Container;
        private List<IconController> icons;

        void Start()
        {
            var iconsInContainer = Container.GetComponentsInChildren<IconController>();
            UnitGroups = new Dictionary<Unit, int>();
            icons = new List<IconController>(iconsInContainer);
        }

        void OnGUI()
        {
            ClearContainer();
            Clear();
            if(selectedUnits.Count == 1)
            {
                ShowFirstUnit();
            }
            else if(SingleUnitType())
            {
                ShowFirstUnit();
                ShowCount();
            }
            else if(selectedUnits.Count > 1)
            {
                GroupUnits();
                ShowCrowdIcon();
                ShowGroupedUnits();
            }
        }

        private void ShowGroupedUnits()
        {
            foreach(var units in UnitGroups)
            {
                var icon = GameObject.Instantiate(selectedIcon, Container.transform);
                icon.SetText(units.Value.ToString());
                icon.SetSprite(units.Key.Icon);
                icons.Add(icon);
            }
        }

        private void ClearContainer()
        {
            foreach(var icon in icons)
            {
                GameObject.Destroy(icon.gameObject);
            }
            icons.Clear();
        }

        private bool SingleUnitType()
        {
            GroupUnits();
            return UnitGroups.Count == 1;
        }

        private void GroupUnits()
        {
            UnitGroups = selectedUnits
                .GroupBy(u => u.unit)
                .ToDictionary(i => i.Key, v => v.Where(u => u.unit == v.Key).Count());
        }

        private void Clear()
        {
            selectedIcon.ResetValues();
        }

        private void ShowFirstUnit()
        {
            selectedIcon.SetSprite(selectedUnits.First().unit.Icon);
        }

        private void ShowCount()
        {
            selectedIcon.SetText(selectedUnits.Count.ToString());
        }

        private void ShowCrowdIcon()
        {
            selectedIcon.SetSprite(crowdIcon);
            selectedIcon.SetText(selectedUnits.Count.ToString());
        }
    }
}

