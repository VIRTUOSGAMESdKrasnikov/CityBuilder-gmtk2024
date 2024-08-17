using System;
using CityBuilder.Interfaces;
using CityBuilder.Spawnables.Scene;
using UnityEngine;

namespace CityBuilder.DataStorage.Storageables
{
    [Serializable]
    public class SpawnableStorageable : IStorageable
    {
        [field:SerializeField] public int ID { get; set; }
        [field:SerializeField] public SceneSpawnable Spawnable { get; private set; }
    }
}