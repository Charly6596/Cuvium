using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public const KeyCode CommandKey = KeyCode.Mouse1;
    public const KeyCode SelectKey = KeyCode.Mouse0;
    public const KeyCode MultiSelectKey = KeyCode.LeftShift;
    public const KeyCode DeselectKey = KeyCode.Escape;
    public UnitManager unitManager;

    void Start()
    {
        unitManager = UnitManager.Singleton;
    }

    void Update()
    {

        if(Input.GetKeyDown(SelectKey))
        {
            if(!GetHittedObject(out var hit)) { return; }
            HandleSelectInput(hit, Input.GetKey(MultiSelectKey));
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
            case Tags.Ground:
                unitManager.DeselectAll();
                break;
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

