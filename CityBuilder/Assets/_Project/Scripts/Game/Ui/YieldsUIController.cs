using System;
using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Bindings;
using CityBuilder.Core.EventBuses.Events;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace CityBuilder.Game.Ui
{
    public class YieldsUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreTMP;
        [SerializeField] private TMP_Text scorePerStepTMP;
        [SerializeField] private TMP_Text citizensTMP;

        private EventBinding<ScoreChangedEvent> _scoreChangedEvent;
        private EventBinding<PlacedBuildingEvent> _placedBuildingEvent;

        private void Awake()
        {
            _scoreChangedEvent = new EventBinding<ScoreChangedEvent>.Builder().WithAction(OnScoreChanged).Build();
            EventBus<ScoreChangedEvent>.Subscribe(_scoreChangedEvent);

            _placedBuildingEvent = new EventBinding<PlacedBuildingEvent>.Builder().WithAction(OnPlacedBuilding).Build();
            EventBus<PlacedBuildingEvent>.Subscribe(_placedBuildingEvent);
        }

        private void OnDestroy()
        {
            EventBus<ScoreChangedEvent>.Unsubscribe(_scoreChangedEvent);
            EventBus<PlacedBuildingEvent>.Unsubscribe(_placedBuildingEvent);
        }

        private void OnScoreChanged(ScoreChangedEvent obj)
        {
            scoreTMP.text = obj.CurrentScore.ToString();
        }
        
        private void OnPlacedBuilding(PlacedBuildingEvent obj)
        {
            scorePerStepTMP.text = $"+{ScoreManager.ScorePerStep} per step";
        }
    }
}