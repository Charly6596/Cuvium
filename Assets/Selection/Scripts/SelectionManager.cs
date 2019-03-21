using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public const KeyCode CommandKey = KeyCode.Mouse1;
    public const KeyCode SelectKey = KeyCode.Mouse0;
    public const KeyCode MultiSelectKey = KeyCode.LeftShift;
    public const KeyCode DeselectKey = KeyCode.Escape;
    public List<UnitController> units;

    void Start()
    {
        units = new List<UnitController>();
    }

    void Update()
    {

        if(Input.GetKeyDown(SelectKey))
        {
            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(!Physics.Raycast(camRay, out var hit)) { return; }
            HandleSelectInput(hit, Input.GetKey(MultiSelectKey));
        }
        else if(Input.GetKeyDown(CommandKey))
        {
            if(units.Count == 0) { return; }

            var camRay = Camera.main.ScreenPointToRay(Input.mousePosition);


            if(!Physics.Raycast(camRay, out var hit)) { return; }
            HandleCommandInput(hit);
        }
        else if(Input.GetKeyDown(DeselectKey))
        {
            HandleDeselectInput();
        }
    }

    private void HandleSelectInput(RaycastHit hit, bool multiSelect = false)
    {
        switch(hit.transform.tag)
        {
            case Tags.Unit:
                SelectUnit(hit.transform.GetComponent<UnitController>(), multiSelect);
                break;
            case Tags.Ground:
                DeselectUnits();
                break;
        }
    }

    private void SelectUnit(UnitController unit, bool multiSelect = false)
    {
        if(!multiSelect)
        {
            DeselectUnits();
        }
        units.Add(unit);
        unit.Select();
    }

    private void DeselectUnits()
    {
        foreach(var unit in units)
        {
            unit.Deselect();
        }
        units.Clear();
    }

    private void HandleCommandInput(RaycastHit hit)
    {
        switch(hit.transform.tag)
        {
            case Tags.Ground:
                MoveUnits(hit.point);
                break;
            case Tags.Unit:
                AttackUnit(hit.transform.GetComponent<UnitController>());
                break;
        }
    }

    private void MoveUnits(Vector3 destination)
    {
        foreach(var unit in units)
        {
            unit.Move(destination);
        }
    }

    private void AttackUnit(UnitController target)
    {
        foreach(var unit in units)
        {
            unit.Attack(target);
        }
    }

    private void HandleDeselectInput()
    {
        
    }

}

