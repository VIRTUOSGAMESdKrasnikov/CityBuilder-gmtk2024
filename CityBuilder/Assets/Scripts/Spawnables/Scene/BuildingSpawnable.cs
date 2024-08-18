using System.Linq;
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

        public bool CanBePlaced()
        {
            if (_scoreCalculator is not HouseCalculator)
            {
                Debug.Log($"for building {name} got score {_scoreCalculator.GetScore(transform)}");
                return _scoreCalculator.GetScore(transform) > 0;
            }

            return true;
        }
        
        public void UpdateModelGhostState(bool isGhost, bool canBePlaced)
        {
            _model.UpdateModelGhostState(isGhost, canBePlaced);
        }
    }
}