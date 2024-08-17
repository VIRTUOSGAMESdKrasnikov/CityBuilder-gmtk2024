using CityBuilder.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CityBuilder.Spawnables.UI
{
    public abstract class UiSpawnable : MonoBehaviour, ISpawnable
    {
        public abstract UniTask<bool> Spawn(int id);
    }
}