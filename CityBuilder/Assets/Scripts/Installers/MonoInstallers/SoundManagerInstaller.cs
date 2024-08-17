using CityBuilder.Game.Sfx;
using UnityEngine;
using Zenject;

namespace CityBuilder.Installers.MonoInstallers
{
    public class SoundManagerInstaller : MonoInstaller
    {
        [SerializeField] private SoundManager _soundManagerInstaller;
        
        public override void InstallBindings()
        {
            Container.Bind<SoundManager>().FromInstance(_soundManagerInstaller).AsSingle();
        }
    }
}