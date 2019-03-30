using UnityEngine;

namespace Cuvium.Core
{
    [RequireComponent(typeof(DragManager))]
    public class SelectionManager : MonoBehaviour
    {
        public const KeyCode CommandKey = KeyCode.Mouse1;
        public const KeyCode SelectKey = KeyCode.Mouse0;
        public const KeyCode MultiSelectKey = KeyCode.LeftShift;
        public const KeyCode DeselectKey = KeyCode.Escape;
        private DragManager dragManager;
        public SelectedUnitCollection unitManager;

        void Start()
        {
            dragManager = GetComponent<DragManager>();
        }

        void Update()
        {

            if(Input.GetKeyDown(SelectKey))
            {
                if(!GetHittedObject(out var hit)) { return; }
                HandleSelectInput(hit, Input.GetKey(MultiSelectKey));
            }
            else if(Input.GetKeyUp(SelectKey))
            {
                HandleSelectInputRelease(Input.GetKey(MultiSelectKey));
            }
            else if(Input.GetKeyDown(CommandKey))
            {
                if(!GetHittedObject(out var hit)) { return; }
                HandleCommandInput(hit);
            }
            else if(Input.GetKeyDown(DeselectKey))
            {
                HandleDeselectInput();
            }
        }

        private bool GetHittedObject(out RaycastHit hit)
        {
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(camRay, out hit);
        }

        private void HandleSelectInput(RaycastHit hit, bool multiSelect = false)
        {
            switch(hit.transform.tag)
            {
                case Tags.Unit:
                    unitManager.Select(hit.transform.GetComponent<UnitController>(), multiSelect);
                    break;
                case Tags.Structure:
                    CreateUnit(hit.transform.GetComponent<BuildingController>());
                    break;
                default:
                    dragManager.StartDragging(Input.mousePosition);
                    break;
            }
        }

        private void CreateUnit(BuildingController building)
        {
            building.CreateUnit();
        }

        private void HandleSelectInputRelease(bool multiSelect)
        {
            var units = FindObjectsOfType<UnitController>();
            if(dragManager.TryGrabUnits(units, out var selected))
            {
                unitManager.Select(selected, multiSelect);
            }
        }

        private void HandleCommandInput(RaycastHit hit)
        {
            switch(hit.transform.tag)
            {
                case Tags.Ground:
                    unitManager.MoveAll(hit.point);
                    break;
                case Tags.Unit:
                    unitManager.Attack(hit.transform.GetComponent<UnitController>());
                    break;
            }
        }

        private void HandleDeselectInput()
        {
            unitManager.DeselectAll();
        }
    }
}

