using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Bindings;
using CityBuilder.Core.EventBuses.Events;
using CityBuilder.Interfaces;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace CityBuilder.Game.Ui
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private DeckController _deckController;
        [SerializeField] private TextMeshProUGUI _quitPlacementModeText;

        [SerializeField] private CanvasGroup _winPanel;
        
        [Inject] private GameManager _gameManager;
        [Inject] private IRuntimeDataProvider _runtimeDataProvider;
        
        private EventBinding<CardClickedEvent> _cardClickedEvent;

        private bool _alreadyAsked = false;
        
        public void Init()
        {
            _winPanel.gameObject.SetActive(false);
            _quitPlacementModeText.gameObject.SetActive(false);

            var builder = new EventBinding<CardClickedEvent>.Builder();
            _cardClickedEvent = builder.WithAction(OnCardClicked).Build();

            EventBus<CardClickedEvent>.Subscribe(_cardClickedEvent);

            _deckController.SetCardsRoster(_runtimeDataProvider.PlayerDeck);

            StepManager.Stepped += OnStepped;
        }

        private void OnStepped()
        {
            if (!_alreadyAsked)
            {
                _alreadyAsked = true;

                if (ScoreManager.ScorePerStep >= 50)
                {
                    _winPanel.alpha = 0;
                    _winPanel.gameObject.SetActive(true);
                    _winPanel.DOFade(1, 1.5f);
                }
            }
        }

        public void Dispose()
        {
            EventBus<CardClickedEvent>.Unsubscribe(_cardClickedEvent);
        }

        public void LeftBuildingMode()
        {
            _quitPlacementModeText.gameObject.SetActive(false);
        }
        
        private void OnCardClicked(CardClickedEvent @event)
        {
            _gameManager.OnUiCardClicked(@event.ClickedId);
            _quitPlacementModeText.gameObject.SetActive(true);
        }
    }
}