using CityBuilder.DataStorage;
using CityBuilder.DataStorage.Storageables;
using CityBuilder.Interfaces;
using CityBuilder.Spawnables.Scene;
using CityBuilder.Spawners;
using UnityEngine;
using Zenject;

namespace CityBuilder.Game.Building
{
    public class BuildingManager : MonoBehaviour
    {
        [Inject] private DiContainer _container;
        [Inject] private IRuntimeDataProvider _runtimeDataProvider;
        
        private ISpawner<SpawnableDataStorage, SpawnableStorageable, BuildingSpawnable> _spawner;
        
        private void Awake()
        {
            _spawner = new BuildingsSpawner();
            _spawner.Init(_runtimeDataProvider.BuildingsStorage, _container);
        }
        
        public async void SpawnBuilding(int id)
        {
            await _spawner.Spawn(new[] { id });
        }
    }
}