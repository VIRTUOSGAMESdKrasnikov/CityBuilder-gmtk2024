using System;
using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Bindings;
using CityBuilder.Core.EventBuses.Events;
using TMPro;
using UnityEngine;

namespace CityBuilder.Game.Ui
{
    public class YieldsUIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreTMP;
        [SerializeField] private TMP_Text scorePerStepTMP;
        [SerializeField] private TMP_Text citizensTMP;

        private EventBinding<ScoreChangedEvent> _scoreChangedEvent;
        private EventBinding<ScorePerStepChangedEvent> _scorePerStepChangedEvent;

        private void Awake()
        {
            _scoreChangedEvent = new EventBinding<ScoreChangedEvent>.Builder().WithAction(OnScoreChanged).Build();
            EventBus<ScoreChangedEvent>.Subscribe(_scoreChangedEvent);

            _scorePerStepChangedEvent = new EventBinding<ScorePerStepChangedEvent>.Builder().WithAction(OnScorePerStepChanged).Build();
            EventBus<ScorePerStepChangedEvent>.Subscribe(_scorePerStepChangedEvent);
        }

        private void OnDestroy()
        {
            EventBus<ScoreChangedEvent>.Unsubscribe(_scoreChangedEvent);
            EventBus<ScorePerStepChangedEvent>.Unsubscribe(_scorePerStepChangedEvent);
        }

        private void OnScoreChanged(ScoreChangedEvent obj)
        {
            scoreTMP.text = obj.CurrentScore.ToString();
        }
        
        private void OnScorePerStepChanged(ScorePerStepChangedEvent obj)
        {
            scorePerStepTMP.text = $"+{obj.CurrentScorePerStep} per step";
        }
    }
}