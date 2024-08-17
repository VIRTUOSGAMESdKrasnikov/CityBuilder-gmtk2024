using System;
using CityBuilder.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CityBuilder.DevPanel
{
    public class DevPanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _buildingIdInput;
        [SerializeField] private Button _spawnBuildingButton;

        [Inject] private BuildingManager _buildingManager;

        private int _idToSpawn;
        
        private void Awake()
        {
            _buildingIdInput.onEndEdit.AddListener(SetBuildingId);
            _spawnBuildingButton.onClick.AddListener(OnBuildButtonClicked);
        }

        private void OnDestroy()
        {
            _buildingIdInput.onEndEdit.RemoveListener(SetBuildingId);
            _spawnBuildingButton.onClick.RemoveListener(OnBuildButtonClicked);
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