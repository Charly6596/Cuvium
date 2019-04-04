namespace Cuvium.Core
{
    /// <summary>
    ///   Provides the ability to join a IJoinable
    /// </summary>
    public interface IJoiner
    {
        void Join(IJoinable joinable);
        void CanJoin(IJoinable joinable);
    }
}

