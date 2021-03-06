namespace Cuvium.Core
{
    /// <summary>
    ///   Provides the behaviour of attack an
    ///   IAttackable target to the implementor
    /// </summary>
    public interface IAttacker
    {
        int AttackPoints { get; set; }
        void Attack(IAttackable target);
    }
}

