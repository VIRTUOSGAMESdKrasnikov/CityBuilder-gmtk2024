using System;
using System.Collections.Generic;
using System.Linq;
using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Bindings;
using CityBuilder.Core.EventBuses.Events;
using CityBuilder.DataStorage;

namespace CityBuilder.Game.Deck
{
    public class PlayerDeck
    {
        public event Action AvailableCardsUpdated;

        public IEnumerable<int> RosterCards { get; private set; } = Enumerable.Empty<int>();
        public IEnumerable<int> AvailableCards { get; private set; } = Enumerable.Empty<int>();

        private BuildingsDataStorage _dataStorage;

        private EventBinding<DevPanelIgnoreScore> _ignoreScoreEvent;
        private EventBinding<ScorePerStepChangedEvent> _scorePerStepChangedEvent;

        public PlayerDeck(BuildingsDataStorage dataStorage, IEnumerable<int> inGameRosterCardsIds)
        {
            _dataStorage = dataStorage;
            RosterCards = inGameRosterCardsIds;

            foreach (int id in inGameRosterCardsIds)
            {
                if (_dataStorage.TryGetItem(id, out var item))
                    if (item.AvailableOnStart)
                        AvailableCards = AvailableCards.Append(item.ID);
            }

            var builder = new EventBinding<DevPanelIgnoreScore>.Builder();
            _ignoreScoreEvent = builder.WithAction(UnlockAllCards).Build();
            EventBus<DevPanelIgnoreScore>.Subscribe(_ignoreScoreEvent);

            _scorePerStepChangedEvent = new EventBinding<ScorePerStepChangedEvent>.Builder()
                .WithAction(OnScorePerStepChanged).Build();
            EventBus<ScorePerStepChangedEvent>.Subscribe(_scorePerStepChangedEvent);
        }

        public void UnlockAllCards(DevPanelIgnoreScore @event)
        {
            if (@event.Ignore)
            {
                AvailableCards = RosterCards;
            }
            else
            {
                AvailableCards = Enumerable.Empty<int>();

                foreach (int id in RosterCards)
                    if (_dataStorage.TryGetItem(id, out var item))
                        if (item.AvailableOnStart)
                            AvailableCards = AvailableCards.Append(item.ID);
            }

            AvailableCardsUpdated?.Invoke();
        }

        private void OnScorePerStepChanged(ScorePerStepChangedEvent obj)
        {
            foreach (int id in RosterCards)
            {
                if (_dataStorage.TryGetItem(id, out var item))
                    if (item.ScorePerStepNeeded <= obj.CurrentScorePerStep)
                        AvailableCards = AvailableCards.Append(item.ID);
            }

            AvailableCardsUpdated?.Invoke();
        }
    }
}