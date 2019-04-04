using UnityEngine;
using UnityEngine.AI;
using Cuvium.Behaviours;
using Cuvium.Commands;
using System.Linq;

namespace Cuvium.Core
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitController : CuviumController, IMoveable, IJoiner, IAttackable, IAttacker
    {
        public Unit unit;
        public Unit currentStats;
        [SerializeField]
        private NavMeshAgent navMesh;

        void Start()
        {
            InitializeStats();
            navMesh.speed = unit.Speed;
        }

        public void Move(Vector3 destination)
        {
            Debug.Log(destination);
            navMesh.SetDestination(destination);
        }

        private void InitializeStats()
        {
            currentStats = ScriptableObject.CreateInstance<Unit>();
            currentStats.Attack = unit.Attack;
            currentStats.Speed = unit.Speed;
            currentStats.Commands = unit.Commands;
        }

        void OnDisable()
        {
        }

        public void Attack(IAttackable target)
        {
            target.GetAttackedBy(this);
        }

        public void GetAttackedBy(IAttacker attacker)
        {
            Health -= attacker.Attack;
        }

        public void Join(IJoinable joinable)
        {
            if(joinable.IsFull()) { return; }
            joinable.BeJoined(this);

        }
    }
}

