namespace Cuvium.Core
{
    /// <summary>
    ///   Provides the behaviour of being joined by a
    ///   IJoiner. Mainly for buildings.
    /// </summary>
    public interface IJoinable
    {
        int Capacity { get; set; }
        void BeJoined(IJoiner joiner);
    }
}

