using System.Collections.Generic;
using System.Linq;
using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Bindings;
using CityBuilder.Core.EventBuses.Events;
using CityBuilder.DataStorage;
using UnityEngine;
using Zenject;

namespace CityBuilder.Game.Ui
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private DummyData _dummyData;
        [SerializeField] private DeckController _deckController;

        [Inject] private GameManager _gameManager;

        private List<int> _availableCardsIds;
        
        private EventBinding<CardClickedEvent> _cardClickedEvent;
        
        public void Init(IEnumerable<int> availableCardsIds)
        {
            var builder = new EventBinding<CardClickedEvent>.Builder();
            _cardClickedEvent = builder.WithAction(OnCardClicked).Build();

            EventBus<CardClickedEvent>.Subscribe(_cardClickedEvent);

            _availableCardsIds = (availableCardsIds == null || !availableCardsIds.Any())
                ? _dummyData.DefaultIds
                : availableCardsIds.ToList();

            _deckController.SetAvailableCards(_availableCardsIds);
        }

        public void Dispose()
        {
            EventBus<CardClickedEvent>.Unsubscribe(_cardClickedEvent);
        }
        
        private void OnCardClicked(CardClickedEvent @event)
        {
            _gameManager.OnUiCardClicked(@event.ClickedId);
            Debug.Log($"card with id {@event.ClickedId} clicked");
        }
    }
}