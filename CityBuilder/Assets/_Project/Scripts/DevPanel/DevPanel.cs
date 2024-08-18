using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Events;
using CityBuilder.Game.Building;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CityBuilder.DevPanel
{
    public class DevPanel : MonoBehaviour
    {
        [SerializeField] private Button _switchButton;

        [SerializeField] private Toggle _ignoreScore;
        
        [SerializeField] private TMP_InputField _buildingIdInput;
        [SerializeField] private Button _spawnBuildingButton;

        [Inject] private BuildingManager _buildingManager;

        private int _idToSpawn;
        
        private void Awake()
        {
#if UNITY_EDITOR
            _switchButton.gameObject.SetActive(true);
            _switchButton.onClick.AddListener(OnSwitchButtonClicked);
#endif
            
            gameObject.SetActive(false);
            
            _buildingIdInput.onEndEdit.AddListener(SetBuildingId);
            _spawnBuildingButton.onClick.AddListener(OnBuildButtonClicked);
            _ignoreScore.onValueChanged.AddListener(OnToggleUpdated);
        }

        private void OnDestroy()
        {
            _buildingIdInput.onEndEdit.RemoveListener(SetBuildingId);
            _spawnBuildingButton.onClick.RemoveListener(OnBuildButtonClicked);
            _ignoreScore.onValueChanged.RemoveListener(OnToggleUpdated);
        }

        private void OnSwitchButtonClicked()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        private void OnToggleUpdated(bool isOn)
        {
            EventBus<DevPanelIgnoreScore>.Publish(new DevPanelIgnoreScore(isOn));
        }

        private void SetBuildingId(string text)
        {
            int.TryParse(text, out _idToSpawn);
        }

        private void OnBuildButtonClicked()
        {
            _buildingManager?.SpawnBuilding(_idToSpawn);
        }
    }
}