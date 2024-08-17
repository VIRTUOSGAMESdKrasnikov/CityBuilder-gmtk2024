using CityBuilder.Interfaces;
using CityBuilder.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CityBuilder.Spawnables.Scene
{
    public class BuildingSpawnable : SceneSpawnable
    {
        [Inject] private IRuntimeDataProvider _runtimeDataProvider;
        
        public override async UniTask<bool> Spawn(int id)
        {
            if (_runtimeDataProvider.ModelStorage.TryGetItem(id, out var model))
            {
                var modelInstantiateProcess = InstantiateAsync(model.Spawnables.Random(), transform);
                await modelInstantiateProcess;
            }
            else
            {
                Debug.LogError($"cant find model for id {id}");
            }

            return true;
        }
    }
}