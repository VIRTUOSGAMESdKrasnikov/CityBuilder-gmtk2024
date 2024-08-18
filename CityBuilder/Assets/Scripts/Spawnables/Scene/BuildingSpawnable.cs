using System.Linq;
using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Events;
using CityBuilder.Interfaces;
using CityBuilder.ScoreCalculators;
using CityBuilder.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CityBuilder.Spawnables.Scene
{
    public class BuildingSpawnable : SceneSpawnable
    {
        [SerializeField] private Transform _radiusShower;
        
        [SerializeField] protected float _range;
        [SerializeField] private ScoreCalculatorBase _scoreCalculator;
        
        [Inject] private IRuntimeDataProvider _runtimeDataProvider;

        private BuildingModelSpawnable _model;

        public override async UniTask<bool> Spawn(int id)
        {
            if (_runtimeDataProvider.ModelStorage.TryGetItem(id, out var model))
            {
                var modelInstantiateProcess = InstantiateAsync(model.Spawnables.Random(), transform);
                await modelInstantiateProcess;

                _model = modelInstantiateProcess.Result.FirstOrDefault() as BuildingModelSpawnable;
            }
            else
            {
                Debug.LogError($"cant find model for id {id}");
            }

            return true;
        }

        public void ShowRaycastRadius()
        {
            _radiusShower.gameObject.SetActive(true);

            _radiusShower.localScale = Vector3.one * _range;
            _radiusShower.localPosition = Vector3.zero;
        }
        
        public bool CanBePlaced()
        {
            if (_scoreCalculator is not HouseCalculator)
            {
                return _scoreCalculator.GetScore(transform, _range) > 0;
            }

            return true;
        }
        
        public void UpdateModelGhostState(bool isGhost, bool canBePlaced)
        {
            _model.UpdateModelGhostState(isGhost, canBePlaced);
        }

        public void Place()
        {
            _radiusShower.gameObject.SetActive(false);
            
            SendPlacedEvent();
            BookCollectables();
        }

        public int GetScore() => _scoreCalculator.GetScore(transform, _range);
        
        private void SendPlacedEvent()
        {
            EventBus<LeftBuildingMode>.Publish(new LeftBuildingMode());
            EventBus<PlacedBuildingEvent>.Publish(new PlacedBuildingEvent(this));
        }

        private void BookCollectables()
        {
            var nearbyResources = Physics.OverlapBox(
                transform.position,
                new Vector3(_range / 2, 100, _range / 2),
                Quaternion.identity,
                LayerMask.GetMask("Collectable"));

            foreach (var collider in nearbyResources)
            {
                if (collider.TryGetComponent<ICollectable>(out var collectable))
                {
                    if (collectable.Id == _scoreCalculator.TargetResourceId)
                    {
                        collectable.IsTaken = true;
                    }
                }
            }
        }
    }
}