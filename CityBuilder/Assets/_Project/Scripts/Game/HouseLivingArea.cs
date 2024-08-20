using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Bindings;
using CityBuilder.Core.EventBuses.Events;
using CityBuilder.Spawnables.Scene;
using UnityEngine;

namespace CityBuilder.Game
{
    [RequireComponent(typeof(BuildingSpawnable))]
    public class HouseLivingArea : MonoBehaviour
    {
        [SerializeField] private float livingSpaceRadius = 5f;
        [SerializeField] private GameObject livingSpaceAreaRadiusObj;

        private EventBinding<EnteredBuildingMode> _enteredBuildingModeEvent;
        private EventBinding<LeftBuildingMode> _leftBuildingMode;

        private void Awake()
        {
            livingSpaceAreaRadiusObj.transform.localPosition = Vector3.zero;
            livingSpaceAreaRadiusObj.transform.localScale = Vector3.one * livingSpaceRadius;

            _enteredBuildingModeEvent = new EventBinding<EnteredBuildingMode>.Builder()
                .WithAction(OnEnteredBuildingMode).Build();
            EventBus<EnteredBuildingMode>.Subscribe(_enteredBuildingModeEvent);

            _leftBuildingMode = new EventBinding<LeftBuildingMode>.Builder().WithAction(OnLeftBuildingMode).Build();
            EventBus<LeftBuildingMode>.Subscribe(_leftBuildingMode);
        }

        private void OnDestroy()
        {
            EventBus<EnteredBuildingMode>.Unsubscribe(_enteredBuildingModeEvent);
            EventBus<LeftBuildingMode>.Unsubscribe(_leftBuildingMode);
        }

        private void OnLeftBuildingMode(LeftBuildingMode obj) => livingSpaceAreaRadiusObj.gameObject.SetActive(false);

        private void OnEnteredBuildingMode(EnteredBuildingMode obj) =>
            livingSpaceAreaRadiusObj.gameObject.SetActive(true);

        public bool IsSuitablePlace(Transform buildingPoint)
        {
            Vector3 position = new Vector3(buildingPoint.transform.position.x, transform.position.y,
                buildingPoint.transform.position.z);
            return Vector3.Distance(position, transform.position) < livingSpaceRadius;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, livingSpaceRadius);
        }
    }
}