using System.Linq;
using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Bindings;
using CityBuilder.Core.EventBuses.Events;
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
        [SerializeField] private GhostPlacementManager _placementManager;
        
        [Inject] private DiContainer _container;
        [Inject] private IRuntimeDataProvider _runtimeDataProvider;

        [Inject] private GameManager _gameManager;
        
        private ISpawner<SpawnableDataStorage, SpawnableStorageable, BuildingSpawnable> _spawner;
        
        private EventBinding<LeftBuildingMode> _leftBuildingModeEvent;
        
        private void Awake()
        {
            _spawner = new BuildingsSpawner();
            _spawner.Init(_runtimeDataProvider.BuildingsStorage, _container);
            
            var builder = new EventBinding<LeftBuildingMode>.Builder();
            _leftBuildingModeEvent = builder.WithAction(OnLeftBuildingMode).Build();
            
            EventBus<LeftBuildingMode>.Subscribe(_leftBuildingModeEvent);
        }

        private void OnDestroy()
        {
            EventBus<LeftBuildingMode>.Unsubscribe(_leftBuildingModeEvent);
        }

        public async void SpawnBuilding(int id)
        {
            var spawnedBuilding = await _spawner.Spawn(new[] { id });
            _placementManager.SetCurrentBuilding(spawnedBuilding.FirstOrDefault());

            EventBus<EnteredBuildingMode>.Publish(new EnteredBuildingMode());
        }

        private void OnLeftBuildingMode()
        {
            _gameManager.LeftBuildingMode();
        }
    }
}