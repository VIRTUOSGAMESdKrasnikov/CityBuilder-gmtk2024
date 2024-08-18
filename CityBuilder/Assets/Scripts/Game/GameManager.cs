using CityBuilder.Game.Building;
using CityBuilder.Game.Ui;
using UnityEngine;
using Zenject;

namespace CityBuilder.Game
{
    public class GameManager : MonoBehaviour
    {
        [Inject] private UiManager _uiManager;
        [Inject] private BuildingManager _buildingManager;

        public void OnUiCardClicked(int id)
        {
            _buildingManager.SpawnBuilding(id);
        }
    }
}