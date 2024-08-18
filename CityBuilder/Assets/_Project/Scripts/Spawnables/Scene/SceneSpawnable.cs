using CityBuilder.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CityBuilder.Spawnables.Scene
{
    public abstract class SceneSpawnable : MonoBehaviour, ISpawnable
    {
        public abstract UniTask<bool> Spawn(int id);
    }
}