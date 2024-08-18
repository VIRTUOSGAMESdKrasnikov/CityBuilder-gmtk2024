using CityBuilder.DataStorage;
using CityBuilder.Interfaces;
using CityBuilder.Game.Deck;
using UnityEngine;

namespace CityBuilder.RuntimeData
{
    public class RuntimeDataProvider : MonoBehaviour, IRuntimeDataProvider
    {
        [SerializeField] private SpawnableDataStorage _buildingsStorage;
        [SerializeField] private ModelDataStorage _modelsStorage;
        [SerializeField] private UiCardsStorage _uiCardStorage;
        [SerializeField] private UiSoundsStorage _uiSoundsStorage;
        [SerializeField] private OrbitalCameraStorage _orbitalCameraStorage;
        [SerializeField] private BuildingsDataStorage _buildingsDataStorage;
        [SerializeField] private CursorStates _cursorStates;
        [SerializeField] private GameRulesStorage _gameRulesStorage;
        
        public SpawnableDataStorage BuildingsStorage => _buildingsStorage;
        public ModelDataStorage ModelStorage => _modelsStorage;
        public UiCardsStorage UiCardsStorage => _uiCardStorage;
        public UiSoundsStorage UiSoundsStorage => _uiSoundsStorage;
        
        public BuildingsDataStorage BuildingsDataStorage => _buildingsDataStorage;
        public PlayerDeck PlayerDeck { get; set; }
        public CursorStates CursorStates => _cursorStates;
        public OrbitalCameraStorage OrbitalCameraStorage => _orbitalCameraStorage;
        public GameRulesStorage RulesStorage => _gameRulesStorage;
    }
}