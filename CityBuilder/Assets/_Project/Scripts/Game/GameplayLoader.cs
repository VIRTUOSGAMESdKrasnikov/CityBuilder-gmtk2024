using System.Linq;
using CityBuilder.DataStorage;
using CityBuilder.Game.Deck;
using CityBuilder.Game.Sfx;
using CityBuilder.Game.Ui;
using CityBuilder.Interfaces;
using UnityEngine;
using Zenject;

namespace CityBuilder.Game
{
    public class GameplayLoader : MonoBehaviour
    {
        [SerializeField] private DummyData _dummyData;
        
        [Inject] private UiManager _uiManager;
        [Inject] private SoundManager _soundManager;
        [Inject] private IRuntimeDataProvider _runtimeDataProvider;
        
        private void Awake()
        {
            // todo here we get list of player cards from previous scene and pass them to managers

            _runtimeDataProvider.PlayerDeck = new PlayerDeck(_runtimeDataProvider.BuildingsDataStorage, _dummyData.DefaultIds);
            
            _uiManager.Init();
            _soundManager.Init();
        }

        private void OnDestroy()
        {
            _uiManager.Dispose();
            _soundManager.Dispose();
        }
    }
}