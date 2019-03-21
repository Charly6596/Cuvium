using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitController : MonoBehaviour
{
    public Unit unit;
    private NavMeshAgent navMesh;
    private Transform highlight;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.speed = unit.Speed;
        highlight = transform.Find("Highlight");
    }

    public void Select()
    {
        highlight.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        highlight.gameObject.SetActive(false);
    }

    public void Move(Vector3 destination)
    {
        navMesh.SetDestination(destination);
    }

    public void Attack(UnitController target)
    {
        Move(target.transform.position);
        Debug.Log("Attacking " + target.name);
    }
}

