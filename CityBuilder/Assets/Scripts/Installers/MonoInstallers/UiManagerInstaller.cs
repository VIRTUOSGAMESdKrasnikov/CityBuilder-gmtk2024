using CityBuilder.Game.Ui;
using UnityEngine;
using Zenject;

namespace CityBuilder.Installers.MonoInstallers
{
    public class UiManagerInstaller : MonoInstaller
    {
        [SerializeField] private UiManager _uiManager;
        
        public override void InstallBindings()
        {
            Container.Bind<UiManager>().FromInstance(_uiManager).AsSingle();
        }
    }
}