using System.Linq;
using CityBuilder.Game.Sfx;
using CityBuilder.Game.Ui;
using UnityEngine;
using Zenject;

namespace CityBuilder.Game
{
    public class GameplayLoader : MonoBehaviour
    {
        [Inject] private UiManager _uiManager;
        [Inject] private SoundManager _soundManager;
        
        private void Awake()
        {
            // todo here we get list of player cards from previous scene and pass them to ui manager
            _uiManager.Init(Enumerable.Empty<int>());
            _soundManager.Init();
        }

        private void OnDestroy()
        {
            _uiManager.Dispose();
            _soundManager.Dispose();
        }
    }
}