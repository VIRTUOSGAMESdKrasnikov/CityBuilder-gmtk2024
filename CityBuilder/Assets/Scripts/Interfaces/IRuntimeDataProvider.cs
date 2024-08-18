using CityBuilder.DataStorage;
using CityBuilder.Game;
using CityBuilder.Game.Deck;

namespace CityBuilder.Interfaces
{
    public interface IRuntimeDataProvider
    {
        public SpawnableDataStorage BuildingsStorage { get; }
        public ModelDataStorage ModelStorage { get; }
        public UiCardsStorage UiCardsStorage { get; }
        public UiSoundsStorage UiSoundsStorage { get; }
        public BuildingsDataStorage BuildingsDataStorage { get; }
        public PlayerDeck PlayerDeck { get; set; }
    }
}