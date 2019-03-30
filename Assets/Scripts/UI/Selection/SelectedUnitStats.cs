using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cuvium.Core
{
    public class SelectedUnitStats : MonoBehaviour
    {
        [SerializeField]
        private Text statUIPrefab;
        [SerializeField]
        private SelectedUnitCollection selectedUnits;

        private List<Text> stats;

        // Start is called before the first frame update
        void Start()
        {
            stats = new List<Text>(GetComponentsInChildren<Text>());
            ClearPanel();
        }

        private void OnGUI()
        {
            if(selectedUnits.Count == 1)
            {
                if(stats.Count > 0) { ClearPanel(); }
                var unit = selectedUnits[0];
                var attack = Instantiate(statUIPrefab, transform);
                var speed = Instantiate(statUIPrefab, transform);
                var faction = Instantiate(statUIPrefab, transform);
                stats.Add(attack);
                stats.Add(speed);
                stats.Add(faction);
                attack.text = $"Attack: {unit.unit.Attack}";
                speed.text = $"Speed: {unit.unit.Speed}";
                faction.text = $"Faction: {unit.unit.Faction.Name}";
            }
            else
            {
                ClearPanel();
            }
        }

        private void ClearPanel()
        {
            foreach(var stat in stats)
            {
                GameObject.Destroy(stat.gameObject);
            }
            stats.Clear();
        }
    }
}

