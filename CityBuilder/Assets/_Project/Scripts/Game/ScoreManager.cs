using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Bindings;
using CityBuilder.Core.EventBuses.Events;
using UnityEngine;

namespace CityBuilder.Game
{
    public class ScoreManager : MonoBehaviour
    {
        public static int Score { get; private set; }
        public static int ScorePerStep { get; private set; }

        private EventBinding<PlacedBuildingEvent> _placedBuildingEvent;

        private void Awake()
        {
            var builder = new EventBinding<PlacedBuildingEvent>.Builder();
            _placedBuildingEvent = builder.WithAction(OnPlacedBuilding).Build();
            EventBus<PlacedBuildingEvent>.Subscribe(_placedBuildingEvent);

            StepManager.Stepped += OnStepped;
        }

        private void OnDestroy()
        {
            EventBus<PlacedBuildingEvent>.Unsubscribe(_placedBuildingEvent);
            StepManager.Stepped -= OnStepped;
        }

        public void AddScore(int count)
        {
            Score += count;
            if (Score <= 0)
                Score = 0;

            var @event = new ScoreChangedEvent(Score);
            EventBus<ScoreChangedEvent>.Publish(@event);
            
            Debug.Log($"Current score: <color=yellow> {Score} </color>");
        }

        private void AddScorePerStep(int count)
        {
            ScorePerStep += count;
            if (ScorePerStep <= 0)
                ScorePerStep = 0;
            
            var @event = new ScorePerStepChangedEvent(ScorePerStep);
            EventBus<ScorePerStepChangedEvent>.Publish(@event);
            
            Debug.Log($"Current score per step: <color=blue> {ScorePerStep} </color>");
        }

        private void OnStepped() => AddScore(ScorePerStep);

        private void OnPlacedBuilding(PlacedBuildingEvent placedBuildingEvent) =>
            AddScorePerStep(placedBuildingEvent.BuildingSpawnable.GetScore());
    }
}