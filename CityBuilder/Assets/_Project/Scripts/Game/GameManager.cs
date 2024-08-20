using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Events;
using CityBuilder.Game.Building;
using CityBuilder.Game.Ui;
using CityBuilder.Interfaces;
using UnityEngine;
using Zenject;

namespace CityBuilder.Game
{
    public class GameManager : MonoBehaviour
    {
        [Inject] private UiManager _uiManager;
        [Inject] private BuildingManager _buildingManager;
        [Inject] private IRuntimeDataProvider _runtimeDataProvider;

        
        public void OnUiCardClicked(int id)
        {
            if (!_runtimeDataProvider.BuildingsDataStorage.TryGetItem(id, out var deckItem))
                return;

            if (ScoreManager.Score >= deckItem.ScoreCost)
                _buildingManager.SpawnBuilding(id);
            else
                EventBus<ErrorClickedEvent>.Publish(new ErrorClickedEvent());
        }

        public void LeftBuildingMode()
        {
            _uiManager.LeftBuildingMode();
        }
    }
}