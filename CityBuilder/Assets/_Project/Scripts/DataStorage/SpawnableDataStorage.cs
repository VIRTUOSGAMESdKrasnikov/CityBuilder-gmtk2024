using CityBuilder.DataStorage.Storageables;
using UnityEngine;

namespace CityBuilder.DataStorage
{
    [CreateAssetMenu(fileName = "SpawnableDataStorage", menuName = "Config/SpawnableDataStorage", order = 0)]
    public class SpawnableDataStorage : DataStorageBase<SpawnableStorageable>
    {
    }
}