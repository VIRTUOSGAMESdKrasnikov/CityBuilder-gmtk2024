using CityBuilder.DataStorage;
using CityBuilder.DataStorage.Storageables;
using CityBuilder.Interfaces;
using CityBuilder.Spawners;
using UnityEngine;
using Zenject;

namespace CityBuilder.Game
{
    public class GameplayLoader : MonoBehaviour
    {
        [Inject] private DiContainer _container;
        [Inject] private IRuntimeDataProvider _runtimeDataProvider;
        
        private ISpawner<SpawnableDataStorage, SpawnableStorageable> _buildingSpawner;

        private void Awake()
        {
            _buildingSpawner = new SpawnerBase();
            _buildingSpawner.Init(_runtimeDataProvider.BuildingsStorage, _container);
        }

        private async void Start()
        {
            await _buildingSpawner.Spawn(new[] { 0, 1, 2 });
        }
    }
}