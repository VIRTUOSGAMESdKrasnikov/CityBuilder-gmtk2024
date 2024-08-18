using CityBuilder.DataStorage;

namespace CityBuilder.Interfaces
{
    public interface IRuntimeDataProvider
    {
        public SpawnableDataStorage BuildingsStorage { get; }
        public ModelDataStorage ModelStorage { get; }
        public UiCardsStorage UiCardsStorage { get; }
        public UiSoundsStorage UiSoundsStorage { get; }
        public OrbitalCameraStorage OrbitalCameraStorage { get; }
    }
}