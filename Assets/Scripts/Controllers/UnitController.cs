using UnityEngine;
using UnityEngine.AI;
using Cuvium.Behaviours;

namespace Cuvium.Core
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitController : CuviumController, IMoveable, IJoiner, IAttackable, IAttacker
    {
        public int Speed { get; set; }
        public int MaxHealth { get; set; }
        public int Health { get; set; }
        public int AttackPoints { get; set; }
        public Unit BaseStats;
        [SerializeField]
        private NavMeshAgent navMesh;

        private void Awake()
        {
            InitializeStats(BaseStats);
        }

        public void Move(Vector3 destination)
        {
            navMesh.SetDestination(destination);
        }

        public override void InitializeStats(CuviumScriptable scriptable)
        {
            if(scriptable is Unit unit)
            {
                AttackPoints = unit.Attack;
                Speed = unit.Speed;
                MaxHealth = unit.Health;
                navMesh.speed = Speed;
            }
        }

        public void Attack(IAttackable target)
        {
            target.GetAttackedBy(this);
        }

        public void GetAttackedBy(IAttacker attacker)
        {
            Health -= attacker.AttackPoints;
        }

        public void Join(IJoinable joinable)
        {
            joinable.BeJoined(this);
        }
    }
}

