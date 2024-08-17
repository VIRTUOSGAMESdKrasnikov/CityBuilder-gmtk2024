using CityBuilder.SceneLoaders;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.MainMenu
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private Button _startGameButton;

        private void OnEnable()
        {
            _startGameButton.onClick.AddListener(OnStartGameButtonClick);
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveListener(OnStartGameButtonClick);
        }

        private void OnStartGameButtonClick()
        {
            _sceneLoader.LoadGameScene();
        }
    }
}