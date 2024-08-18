using CityBuilder.DataStorage;
using CityBuilder.Game.Deck;
using CityBuilder.Interfaces;
using UnityEngine;

namespace CityBuilder.RuntimeData
{
    public class RuntimeDataProvider : MonoBehaviour, IRuntimeDataProvider
    {
        [SerializeField] private SpawnableDataStorage _buildingsStorage;
        [SerializeField] private ModelDataStorage _modelsStorage;
        [SerializeField] private UiCardsStorage _uiCardStorage;
        [SerializeField] private UiSoundsStorage _uiSoundsStorage;
        [SerializeField] private BuildingsDataStorage _buildingsDataStorage;

        public SpawnableDataStorage BuildingsStorage => _buildingsStorage;
        public ModelDataStorage ModelStorage => _modelsStorage;
        public UiCardsStorage UiCardsStorage => _uiCardStorage;
        public UiSoundsStorage UiSoundsStorage => _uiSoundsStorage;
        public BuildingsDataStorage BuildingsDataStorage => _buildingsDataStorage;
        public PlayerDeck PlayerDeck { get; set; }
    }
}