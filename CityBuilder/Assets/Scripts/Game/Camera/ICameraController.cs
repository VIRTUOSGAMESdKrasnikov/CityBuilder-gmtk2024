namespace CityBuilder.Game.Camera
{
    public interface ICameraController
    {
        void Init();
        void Dispose();
        void Tick();
    }
}