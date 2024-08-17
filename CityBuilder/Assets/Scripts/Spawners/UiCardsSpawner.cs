using System.Collections.Generic;
using CityBuilder.DataStorage;
using CityBuilder.DataStorage.Storageables;
using CityBuilder.Interfaces;
using CityBuilder.Spawnables.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CityBuilder.Spawners
{
    public class UiCardsSpawner: ISpawner<UiCardsStorage, UiCardStorageable, UiCardSpawnable>
    {
        private UiCardsStorage _spawnableStorage;
        private DiContainer _container;

        public void Init(UiCardsStorage dataStorage, DiContainer container)
        {
            _spawnableStorage = dataStorage;
            _container = container;
        }

        public async UniTask<IEnumerable<UiCardSpawnable>> Spawn(IEnumerable<int> requestedIds)
        {
            var instantiatedElements = new List<UiCardSpawnable>();

            foreach (var id in requestedIds)
            {
                if (_spawnableStorage.TryGetItem(id, out var storageable))
                {
                    var instantiated = _container.InstantiatePrefab(storageable.Spawnable).GetComponent<ISpawnable>();
                    await instantiated.Spawn(id);
                    instantiatedElements.Add(instantiated as UiCardSpawnable);
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