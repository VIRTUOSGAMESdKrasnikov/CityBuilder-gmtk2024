using CityBuilder.Spawnables.Scene;
using UnityEngine;

namespace CityBuilder.Game.Building
{
    public class GhostPlacementManager : MonoBehaviour
    {
        private const float DEFAULT_HEIGHT_OFFSET = 3;
        
        [SerializeField] private bool _canBePlaced;
        
        private BuildingSpawnable _currentBuilding;
        private Camera _mainCamera;

        private RaycastHit _hit;
        
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void SetCurrentBuilding(BuildingSpawnable building)
        {
            _currentBuilding = building;
        }

        private void Update()
        {
            if (_currentBuilding != null)
            {
                MoveBuilding();
                if (CheckPlace())
                {
                    PlaceBuilding();
                }
            }
        }

        private void MoveBuilding()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit))
            {
                _currentBuilding.transform.position = new Vector3(_hit.point.x, _hit.point.y + DEFAULT_HEIGHT_OFFSET, _hit.point.z);
            }
        }

        private bool CheckPlace()
        {
            bool canBePlaced = CanBePlaced();
            _currentBuilding.UpdateModelGhostState(true, canBePlaced);

            return canBePlaced && Input.GetMouseButtonDown(0);
        }
        
        private void PlaceBuilding()
        {
            if (Physics.Raycast(_currentBuilding.transform.position, Vector3.down, out var hit))
            {
                _currentBuilding.UpdateModelGhostState(false, true);
                _currentBuilding.transform.position = hit.point;
            }
            _currentBuilding = null;
        }

        private bool CanBePlaced()
        {
            if (_hit.transform == null)
            {
                return false;
            }
            
            if (_hit.transform.gameObject.layer == 6)
            {
                return true;
            }

            return false;
        }
    }
}