using UnityEngine;
using Cuvium.InputManager;

namespace Cuvium.Core
{
    [RequireComponent(typeof(DragManager))]
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField]
        private KeyCodeVariable RightClick;
        [SerializeField]
        private KeyCodeVariable LeftClick;
        [SerializeField]
        private KeyCodeVariable MultiSelectKey;
        [SerializeField]
        private KeyCodeVariable CancelKey;
        [SerializeField]
        private Player Player;
        private DragManager dragManager;

        public void OnKeyUp(KeyCode key)
        {
            if(key == LeftClick.Value)
            {
                HandleSelectInputRelease(Input.GetKey(MultiSelectKey.Value));
            }
        }

        public void OnKeyDown(KeyCode key)
        {
            if(key == LeftClick.Value)
            {
                if(!GetHittedObject(out var hit)) { return; }
                HandleSelectInput(hit);
            }
            else if(key == RightClick.Value)
            {
                if(!GetHittedObject(out var hit)) { return; }
                HandleCommandInput(hit);
            }
            else if(key == CancelKey.Value)
            {
                HandleDeselectInput();
            }
        }

        public void OnKeyHold(KeyCode key)
        {
        }

        private void Start()
        {
            dragManager = GetComponent<DragManager>();
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
                    var controller = hit.transform.GetComponent<UnitController>();
                    Player.SelectedObjects.Select(controller, multiSelect);
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
                Player.SelectedObjects.Select(selected, multiSelect);
            }
        }

        private void HandleCommandInput(RaycastHit hit)
        {
            switch(hit.transform.tag)
            {
                case Tags.Ground:
                    Player.SelectedObjects.MoveAll(hit.point);
                    //unitManager.Command(MoveCommand);
                    break;
                case Tags.Unit:
                    // unitManager.Attack(hit.transform.GetComponent<UnitController>());
                    break;
            }
        }

        private void HandleDeselectInput()
        {
            Player.SelectedObjects.DeselectAll();
        }
    }
}

