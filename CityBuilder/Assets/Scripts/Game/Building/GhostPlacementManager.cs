using CityBuilder.Spawnables.Scene;
using UnityEngine;

namespace CityBuilder.Game.Building
{
    public class GhostPlacementManager : MonoBehaviour
    {
        private Transform _currentBuilding;
        private Camera _mainCamera;

        private const float DEFAULT_HEIGHT_OFFSET = 3;
        
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void SetCurrentBuilding(BuildingSpawnable building)
        {
            _currentBuilding = building.transform;
        }

        private void Update()
        {
            MoveBuilding();
            if (CheckPlace())
            {
                PlaceBuilding();
            }
        }

        private void MoveBuilding()
        {
            if (_currentBuilding != null)
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    _currentBuilding.transform.position = new Vector3(hit.point.x, hit.point.y + DEFAULT_HEIGHT_OFFSET, hit.point.z);
                }
            }
        }
        
        private bool CheckPlace()
        {
            return Input.GetMouseButtonDown(0);
        }
        
        private void PlaceBuilding()
        {
            if (Physics.Raycast(_currentBuilding.position, Vector3.down, out var hit))
            {
                _currentBuilding.position = hit.point;
            }
            _currentBuilding = null;
        }
    }
}