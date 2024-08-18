using CityBuilder.Game;
using Zenject;

namespace CityBuilder.Installers.MonoInstallers
{
    public class GameManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromNewComponentOnNewGameObject().AsSingle();
        }
    }
}