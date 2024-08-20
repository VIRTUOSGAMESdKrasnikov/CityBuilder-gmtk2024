using System.Collections.Generic;
using System.Linq;
using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Bindings;
using CityBuilder.Core.EventBuses.Events;
using CityBuilder.Interfaces;
using CityBuilder.Utils;
using UnityEngine;
using Zenject;

namespace CityBuilder.Game.Sfx
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _mainMusicSource;
        [SerializeField] private List<AudioSource> _sfxSlots;
        [SerializeField] private GameObject _sfxSlotsParent;

        [Inject] private IRuntimeDataProvider _runtimeDataProvider;
        
        private EventBinding<MouseOverCardEvent> _mouseOverCardEvent;
        private EventBinding<MouseLeftCardEvent> _mouseLeftCardEvent;
        private EventBinding<LeftBuildingMode> _leftBuildingModeEvent;
        
        public void Init()
        {
            var mouseOverCardBuilder = new EventBinding<MouseOverCardEvent>.Builder();
            var mouseLeftCardBuilder = new EventBinding<MouseLeftCardEvent>.Builder();
            var leftBuildingModeBuilder = new EventBinding<LeftBuildingMode>.Builder();

            _mouseOverCardEvent = mouseOverCardBuilder.WithAction(PlayMouseOverCardSound).Build();
            _mouseLeftCardEvent = mouseLeftCardBuilder.WithAction(PlayMouseLeftCardSound).Build();
            _leftBuildingModeEvent = leftBuildingModeBuilder.WithAction(PlayLeftBuildingModeSound).Build();

            EventBus<MouseOverCardEvent>.Subscribe(_mouseOverCardEvent);
            EventBus<MouseLeftCardEvent>.Subscribe(_mouseLeftCardEvent);
            EventBus<LeftBuildingMode>.Subscribe(_leftBuildingModeEvent);

            StepManager.Stepped += PlayStepSound;
        }



        public void Dispose()
        {
            EventBus<MouseOverCardEvent>.Unsubscribe(_mouseOverCardEvent);
            EventBus<MouseLeftCardEvent>.Unsubscribe(_mouseLeftCardEvent);
            EventBus<LeftBuildingMode>.Unsubscribe(_leftBuildingModeEvent);
            
            StepManager.Stepped -= PlayStepSound;
        }

        private void PlayMouseOverCardSound()
        {
            var availableSlot = GetAvailableSlot();
            availableSlot.clip = _runtimeDataProvider.UiSoundsStorage.MouseOverCardSounds.Random();
            
            availableSlot.Play();
        }

        private void PlayMouseLeftCardSound()
        {
            var availableSlot = GetAvailableSlot();
            availableSlot.clip = _runtimeDataProvider.UiSoundsStorage.MouseLeftCardSounds.Random();
            
            availableSlot.Play();
        }
        
        private void PlayLeftBuildingModeSound()
        {
            var availableSlot = GetAvailableSlot();
            availableSlot.clip = _runtimeDataProvider.UiSoundsStorage.BuildingPlacedSounds.Random();

            availableSlot.Play();
        }
        
        private void PlayStepSound()
        {
            var availableSlot = GetAvailableSlot();
            availableSlot.clip = _runtimeDataProvider.UiSoundsStorage.StepSounds.Random();

            availableSlot.Play();
        }
        
        private AudioSource GetAvailableSlot()
        {
            var availableSlots = _sfxSlots.Where(slot => !slot.isPlaying);

            if (!availableSlots.Any())
            {
                var newSlot = _sfxSlotsParent.AddComponent<AudioSource>();
                
                _sfxSlots.Add(newSlot);
                availableSlots = availableSlots.Append(newSlot);
            }

            return availableSlots.FirstOrDefault();
        }
    }
}