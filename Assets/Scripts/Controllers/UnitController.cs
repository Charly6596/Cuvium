using UnityEngine;
using UnityEngine.AI;

namespace Cuvium.Core
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitController : MonoBehaviour
    {
        public Unit unit;
        public Unit currentStats;
        public Player Owner;
        [SerializeField]
        private NavMeshAgent navMesh;
        [SerializeField]
        private Transform highlight;

        void Start()
        {
            InitializeStats();
            navMesh.speed = unit.Speed;
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
            Debug.Log("Move unit");
            navMesh.SetDestination(destination);
        }

        private void InitializeStats()
        {
            currentStats = ScriptableObject.CreateInstance<Unit>();
            currentStats.Attack = unit.Attack;
            currentStats.Speed = unit.Speed;
            Owner.AddUnit(this);
        }

        void OnDisable()
        {
            Owner.RemoveUnit(this);
        }

        public void Attack(UnitController target)
        {
            Move(target.transform.position);
        }
    }
}

