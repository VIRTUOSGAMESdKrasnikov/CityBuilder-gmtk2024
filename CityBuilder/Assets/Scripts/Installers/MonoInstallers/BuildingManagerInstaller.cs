using CityBuilder.Game;
using CityBuilder.Game.Building;
using UnityEngine;
using Zenject;

namespace CityBuilder.Installers.MonoInstallers
{
    public class BuildingManagerInstaller : MonoInstaller
    {
        [SerializeField] private BuildingManager _buildingManager;
        
        public override void InstallBindings()
        {
            Container.Bind<BuildingManager>().FromInstance(_buildingManager).AsSingle();
        }
    }
}