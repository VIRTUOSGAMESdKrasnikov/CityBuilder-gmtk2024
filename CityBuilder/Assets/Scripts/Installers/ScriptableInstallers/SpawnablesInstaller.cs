using CityBuilder.DataStorage;
using UnityEngine;
using Zenject;

namespace CityBuilder.Installers.ScriptableInstallers
{
    [CreateAssetMenu(fileName = "SpawnablesInstaller", menuName = "Installers/SpawnablesInstaller", order = 0)]
    public class SpawnablesInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private SpawnableDataStorage _spawnablesStorage;

        public override void InstallBindings()
        {
            Container.Bind<SpawnableDataStorage>().FromInstance(_spawnablesStorage).AsSingle();
        }
    }
}