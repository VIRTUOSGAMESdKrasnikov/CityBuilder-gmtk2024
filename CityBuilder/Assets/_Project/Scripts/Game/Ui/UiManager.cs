using System.Collections.Generic;
using System.Linq;
using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Bindings;
using CityBuilder.Core.EventBuses.Events;
using CityBuilder.Interfaces;
using TMPro;
using UnityEngine;
using Zenject;

namespace CityBuilder.Game.Ui
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private DeckController _deckController;

        [SerializeField] private TextMeshProUGUI _quitPlacementModeText;
        
        [Inject] private GameManager _gameManager;
        [Inject] private IRuntimeDataProvider _runtimeDataProvider;
        
        private EventBinding<CardClickedEvent> _cardClickedEvent;
        
        public void Init()
        {
            _quitPlacementModeText.gameObject.SetActive(false);

            var builder = new EventBinding<CardClickedEvent>.Builder();
            _cardClickedEvent = builder.WithAction(OnCardClicked).Build();

            EventBus<CardClickedEvent>.Subscribe(_cardClickedEvent);

            _deckController.SetCardsRoster(_runtimeDataProvider.PlayerDeck);
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