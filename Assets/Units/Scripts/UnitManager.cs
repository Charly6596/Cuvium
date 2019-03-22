using System.Collections.Generic;
using UnityEngine;

public class UnitManager
{
    private List<UnitController> units;
    private static UnitManager _instance;
    public static UnitManager Singleton
    {
        get
        {
            if(_instance is null)
            {
                _instance = new UnitManager();
            }
            return _instance;
        }
    }

    private UnitManager()
    {
        units = new List<UnitController>();
    }

    public void Select(UnitController unit, bool multiSelect = false)
    {
        if(!multiSelect)
        {
            DeselectAll();
        }
        units.Add(unit);
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
            this.units.Add(unit);
            unit.Select();
        }
    }

    public void DeselectAll()
    {
        foreach(var unit in units)
        {
            unit.Deselect();
        }
        units.Clear();
    }

    public void MoveAll(Vector3 destination)
    {
        foreach(var unit in units)
        {
            unit.Move(destination);
        }
    }

    public void Attack(UnitController target)
    {
        foreach(var unit in units)
        {
            unit.Attack(target);
        }
    }

    public int Count()
        => units.Count;
}

