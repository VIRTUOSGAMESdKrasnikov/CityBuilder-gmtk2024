using CityBuilder.DataStorage.Storageables;
using UnityEngine;

namespace CityBuilder.DataStorage
{
    [CreateAssetMenu(fileName = "ModelDataStorage", menuName = "Config/Visuals/ModelDataStorage", order = 0)]
    public class ModelDataStorage : DataStorageBase<ModelStorageable>
    {
    }
}