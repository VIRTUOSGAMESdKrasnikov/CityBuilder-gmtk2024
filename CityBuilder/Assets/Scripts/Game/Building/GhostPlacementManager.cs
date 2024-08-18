using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Events;
using CityBuilder.Spawnables.Scene;
using DG.Tweening;
using UnityEngine;

namespace CityBuilder.Game.Building
{
    public class GhostPlacementManager : MonoBehaviour
    {
        private const float DEFAULT_HEIGHT_OFFSET = 3;

        private BuildingSpawnable _currentBuilding;
        private UnityEngine.Camera _mainCamera;

        private RaycastHit _hit;
        
        private void Awake()
        {
            _mainCamera = UnityEngine.Camera.main;
        }

        public void SetCurrentBuilding(BuildingSpawnable building)
        {
            if (_currentBuilding != null)
            {
                Destroy(_currentBuilding.gameObject);
            }

            _currentBuilding = building;
            _currentBuilding.ShowRaycastRadius();
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

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Destroy(_currentBuilding.gameObject);
                    EventBus<LeftBuildingMode>.Publish(new LeftBuildingMode());
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
            bool enoughSpaceAndIsOnMap = IsEnoughPlace();
            bool enoughScore = _currentBuilding.CanBePlaced();

            _currentBuilding.UpdateModelGhostState(true, enoughSpaceAndIsOnMap && enoughScore);

            return enoughScore && enoughSpaceAndIsOnMap && Input.GetMouseButtonDown(0);
        }
        
        private void PlaceBuilding()
        {
            if (Physics.Raycast(_currentBuilding.transform.position, Vector3.down, out var hit))
            {
                _currentBuilding.transform.DOJump(hit.point, 0.5f, 2, 0.2f);
                _currentBuilding.UpdateModelGhostState(false, true);
                _currentBuilding.Place();
            }
            _currentBuilding = null;
        }

        private bool IsEnoughPlace()
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