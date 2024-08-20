using System;
using System.Linq;
using CityBuilder.Core;
using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Events;
using CityBuilder.Interfaces;
using CityBuilder.ScoreCalculators;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CityBuilder.Spawnables.Scene
{
    public class BuildingSpawnable : SceneSpawnable
    {
        [SerializeField] private Transform _radiusShower;
        [SerializeField] private ParticleSystem _onPlaceParticles;

        [SerializeField] protected float _radius;
        [SerializeField] private ScoreCalculatorBase _scoreCalculator;

        [Inject] private IRuntimeDataProvider _runtimeDataProvider;

        public bool IsModelVisible => _model.IsVisible;

        private BuildingModelSpawnable _model;
        public int ID { get; private set; }
        public int COLLECTABLE_ID { get; private set; }


        public override async UniTask<bool> Spawn(int id)
        {
            _onPlaceParticles.gameObject.SetActive(false);
            ID = id;
            COLLECTABLE_ID = ID;
            
            if (_runtimeDataProvider.ModelStorage.TryGetItem(id, out var model))
            {
                // somehow webgl cant properly run InstantiateAsync (maybe javascript)
#if UNITY_WEBGL
                _model = Instantiate(model.Spawnables.FirstOrDefault(), transform) as BuildingModelSpawnable;
#else
                var modelInstantiateProcess = InstantiateAsync(model.Spawnables.FirstOrDefault(), transform);
                await modelInstantiateProcess;

                _model = modelInstantiateProcess.Result.FirstOrDefault() as BuildingModelSpawnable;
#endif
            }
            else
            {
                Debug.LogError($"cant find model for id {id}");
            }

            return true;
        }

        public void ShowRadius()
        {
            _radiusShower.gameObject.SetActive(true);

            _radiusShower.localScale = Vector3.one * (_radius * 2f);
            _radiusShower.localPosition = Vector3.zero;
        }

        public void HideRadius()
        {
            _radiusShower.gameObject.SetActive(false);
        }

        public bool CanBePlaced()
        {
            if (_scoreCalculator is not HouseCalculator)
                return _scoreCalculator.GetScore(transform, _radius) > 0;

            return true;
        }

        public void UpdateModelGhostState(bool isGhost, bool canBePlaced)
            => _model.UpdateModelGhostState(isGhost, canBePlaced);

        public void HideModel()
        {
            _model.Hide();
            HideRadius();
        }

        public void ShowModel()
        {
            _model.Show();
            ShowRadius();
        }

        public void Place()
        {
            HideRadius();

            _onPlaceParticles.gameObject.SetActive(true);
            _onPlaceParticles.Play();

            SendPlacedEvent();
            BookCollectables();
        }

        public int GetScore() => _scoreCalculator.GetScore(transform, _radius);

        private void SendPlacedEvent()
        {
            EventBus<LeftBuildingMode>.Publish(new LeftBuildingMode());
            EventBus<PlacedBuildingEvent>.Publish(new PlacedBuildingEvent(this));
        }

        private void BookCollectables()
        {
            var nearbyResources = Physics.OverlapSphere(
                transform.position, _radius,
                LayerMask.GetMask(Constants.COLLECTABLE_LAYER_NAME));

            foreach (var collider in nearbyResources)
                if (collider.TryGetComponent<ICollectable>(out var collectable))
                    if (collectable.Id == _scoreCalculator.TargetResourceId)
                        collectable.IsTaken = true;
        }

        public void SetCollidersActivation(bool isActive)
        {
            var colls = gameObject.GetComponentsInChildren<Collider>();
            foreach (var coll in colls) coll.enabled = isActive;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_radiusShower.transform.position, _radius);
        }
    }
}