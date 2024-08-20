﻿using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Bindings;
using CityBuilder.Core.EventBuses.Events;
using UnityEngine;

namespace CityBuilder.Game.Collectables
{
    public class HighlightManager : MonoBehaviour
    {
        private Collectable[] _levelCollectables;
        private EventBinding<EnteredBuildingMode> _enteredBuildingModeEvent;
        private EventBinding<LeftBuildingMode> _leftBuildingModeEvent;

        private void Awake()
        {
            FindResources();
            
            var enteredBuilder = new EventBinding<EnteredBuildingMode>.Builder();
            var leftBuilder = new EventBinding<LeftBuildingMode>.Builder();

            _enteredBuildingModeEvent = enteredBuilder.WithAction(OnEnteredBuildingMode).Build();
            _leftBuildingModeEvent = leftBuilder.WithAction(OnLeftBuildingMode).Build();

            EventBus<EnteredBuildingMode>.Subscribe(_enteredBuildingModeEvent);
            EventBus<LeftBuildingMode>.Subscribe(_leftBuildingModeEvent);
        }

        private void OnDestroy()
        {
            EventBus<EnteredBuildingMode>.Unsubscribe(_enteredBuildingModeEvent);
            EventBus<LeftBuildingMode>.Unsubscribe(_leftBuildingModeEvent);
        }

        // TODO: need to be called each island init.
        private void FindResources()
        {
            _levelCollectables = FindObjectsOfType<Collectable>();
        }

        private void OnEnteredBuildingMode(EnteredBuildingMode enteredBuildingMode)
        {
            foreach (var resource in _levelCollectables)
                resource.ChangeView(resource.Id == enteredBuildingMode.BuildingSpawnable.COLLECTABLE_ID);
        }

        private void OnLeftBuildingMode()
        {
            foreach (var resource in _levelCollectables)
                resource.ChangeView(false);
        }
    }
}