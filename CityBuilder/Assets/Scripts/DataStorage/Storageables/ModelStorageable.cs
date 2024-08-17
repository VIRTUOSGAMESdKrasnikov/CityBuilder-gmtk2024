using System;
using CityBuilder.Interfaces;
using CityBuilder.Spawnables.Scene;
using UnityEngine;

namespace CityBuilder.DataStorage.Storageables
{
    [Serializable]
    public class ModelStorageable : IStorageable
    {
        [field:SerializeField] public int ID { get; set; }
        [field:SerializeField] public SceneSpawnable[] Spawnables { get; private set; }
    }
}