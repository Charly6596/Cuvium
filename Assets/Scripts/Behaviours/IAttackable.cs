namespace Cuvium.Core
{
    /// <summary>
    ///  Provides the behaviour of being attacked
    ///  to the implementor
    /// </summary>
    public interface IAttackable
    {
        int Health { get; set; }
        int MaxHealth { get; set; }
        void GetAttackedBy(IAttacker attacker);
    }
}

