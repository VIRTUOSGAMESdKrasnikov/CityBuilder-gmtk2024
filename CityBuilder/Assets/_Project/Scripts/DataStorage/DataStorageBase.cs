using System.Collections.Generic;
using CityBuilder.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace CityBuilder.DataStorage
{
    public abstract class DataStorageBase<T> : ScriptableObject where T : IStorageable
    {
        [FormerlySerializedAs("_storageables")] [SerializeField] private T[] storageables;

        public bool TryGetItem(int id, out T item)
        {
            foreach (var storageable in storageables)
            {
                if (storageable.ID == id)
                {
                    item = storageable;
                    return true;
                }
            }

            item = default;
            return false;
        }

        public IEnumerable<T> GetAllStorageables()
        {
            foreach (T storageable in storageables)
            {
                yield return storageable;
            }
        }
    }
}