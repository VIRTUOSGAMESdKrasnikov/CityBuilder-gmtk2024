using CityBuilder.Interfaces;
using CityBuilder.Spawnables.UI;
using UnityEngine;

namespace CityBuilder.DataStorage.Storageables
{
    [System.Serializable]
    public class UiCardStorageable : IStorageable
    {
        [field:SerializeField] public int ID { get; set; }
        [field:SerializeField] public UiCardSpawnable Spawnable { get; private set; }
    }
}