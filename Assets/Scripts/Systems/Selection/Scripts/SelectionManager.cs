using UnityEngine;
using Cuvium.Commands;
using Cuvium.InputManager;

namespace Cuvium.Core
{
    [RequireComponent(typeof(DragManager))]
    public class SelectionManager : MonoBehaviour
    {
        public Command MoveCommand;
        public KeyCodeVariable RightClick;
        public KeyCodeVariable LeftClick;
        public KeyCodeVariable MultiSelectKey;
        public KeyCodeVariable CancelKey;
        private DragManager dragManager;
        public KeyCodeGameEventListener keyUp;
        public SelectedObjectCollection unitManager;

        public void OnKeyUp(KeyCode key)
        {
            Debug.Log("Key up: " + key);
        }

        public void OnKeyDown(KeyCode key)
        {
            Debug.Log("Key Down: " + key);
        }

        public void OnKeyHold(KeyCode key)
        {
            Debug.Log("Hold key: " + key);
        }

        void Start()
        {
            dragManager = GetComponent<DragManager>();
        }

        void Update()
        {

            if(Input.GetKeyDown(LeftClick))
            {
                if(!GetHittedObject(out var hit)) { return; }
                HandleSelectInput(hit, Input.GetKey(MultiSelectKey));
            }
            else if(Input.GetKeyUp(LeftClick))
            {
                HandleSelectInputRelease(Input.GetKey(MultiSelectKey));
            }
            else if(Input.GetKeyDown(RightClick))
            {
                if(!GetHittedObject(out var hit)) { return; }
                HandleCommandInput(hit);
            }
            else if(Input.GetKeyDown(CancelKey))
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
                  //  unitManager.MoveAll(hit.point);
                    //unitManager.Command(MoveCommand);
                    break;
                case Tags.Unit:
                   // unitManager.Attack(hit.transform.GetComponent<UnitController>());
                    break;
            }
        }

        private void HandleDeselectInput()
        {
            unitManager.DeselectAll();
        }
    }
}

