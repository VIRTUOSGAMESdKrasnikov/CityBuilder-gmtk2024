using System.Collections.Generic;
using CityBuilder.DataStorage;
using CityBuilder.DataStorage.Storageables;
using CityBuilder.Interfaces;
using CityBuilder.Spawnables.Scene;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CityBuilder.Spawners
{
    public class BuildingsSpawner : ISpawner<SpawnableDataStorage, SpawnableStorageable, SceneSpawnable>
    {
        private SpawnableDataStorage _spawnableStorage;
        private DiContainer _container;
        
        public void Init(SpawnableDataStorage dataStorage, DiContainer container)
        {
            _spawnableStorage = dataStorage;
            _container = container;
        }

        public async UniTask<IEnumerable<SceneSpawnable>> Spawn(IEnumerable<int> requestedIds)
        {
            var instantiatedElements = new List<SceneSpawnable>();
            
            foreach (var id in requestedIds)
            {
                if (_spawnableStorage.TryGetItem(id, out var storageable))
                {
                    var instantiated = _container.InstantiatePrefab(storageable.Spawnable).GetComponent<ISpawnable>();
                    await instantiated.Spawn(id);
                    instantiatedElements.Add(instantiated as SceneSpawnable);
                }
                else
                {
                    Debug.LogError($"cant find spawnable for id {id}");
                }
            }

            return instantiatedElements;
        }
    }
}