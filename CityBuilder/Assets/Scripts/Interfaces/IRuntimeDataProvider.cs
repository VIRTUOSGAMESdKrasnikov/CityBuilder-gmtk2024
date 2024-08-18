using CityBuilder.DataStorage;
using CityBuilder.Game.Deck;

namespace CityBuilder.Interfaces
{
    public interface IRuntimeDataProvider
    {
        public SpawnableDataStorage BuildingsStorage { get; }
        public ModelDataStorage ModelStorage { get; }
        public UiCardsStorage UiCardsStorage { get; }
        public UiSoundsStorage UiSoundsStorage { get; }
        public OrbitalCameraStorage OrbitalCameraStorage { get; }
        public BuildingsDataStorage BuildingsDataStorage { get; }
        public PlayerDeck PlayerDeck { get; set; }
        public CursorStates CursorStates { get; }
    }
}