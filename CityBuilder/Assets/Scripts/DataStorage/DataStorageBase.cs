using System.Collections.Generic;
using CityBuilder.Interfaces;
using UnityEngine;

namespace CityBuilder.DataStorage
{
    public abstract class DataStorageBase<T> : ScriptableObject where T : IStorageable
    {
        [SerializeField] private T[] _storageables;

        public bool TryGetItem(int id, out T item)
        {
            foreach (var storageable in _storageables)
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
            foreach (T storageable in _storageables)
            {
                yield return storageable;
            }
        }
    }
}