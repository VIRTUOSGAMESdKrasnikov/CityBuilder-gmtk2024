using CityBuilder.Interfaces;
using CityBuilder.RuntimeData;
using UnityEngine;
using Zenject;

namespace CityBuilder.Installers.MonoInstallers
{
    public class RuntimeDataInstaller : MonoInstaller
    {
        [SerializeField] private RuntimeDataProvider _runtimeDataProvider;
        
        public override void InstallBindings()
        {
            Container.Bind<IRuntimeDataProvider>().To<RuntimeDataProvider>().FromInstance(_runtimeDataProvider).AsSingle();
        }
    }
}