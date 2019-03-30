using UnityEngine;

namespace Cuvium.Core
{
    [CreateAssetMenu(
                     fileName = "UnitCollection.asset",
                     menuName = "Cuvium/UnitCollection",
                     order = 120)]
    public class UnitCollection : Collection<UnitController>
    {
    }
}
