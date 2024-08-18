using System;
using System.Collections.Generic;
using System.Linq;
using CityBuilder.DataStorage;

namespace CityBuilder.Game.Deck
{
    public class PlayerDeck
    {
        public event Action AvailableCardsUpdated;
        
        public IEnumerable<int> RosterCards { get; private set; } = Enumerable.Empty<int>();
        public IEnumerable<int> AvailableCards { get; private set; } = Enumerable.Empty<int>();
     
        private BuildingsDataStorage _dataStorage;
        
        public PlayerDeck(BuildingsDataStorage dataStorage, IEnumerable<int> inGameRosterCardsIds)
        {
            _dataStorage = dataStorage;
            RosterCards = inGameRosterCardsIds;

            foreach (int id in inGameRosterCardsIds)
            {
                if (_dataStorage.TryGetItem(id, out var item))
                {
                    if (item.AvailableOnStart)
                    {
                        AvailableCards = AvailableCards.Append(item.ID);
                    }
                }
            }
        }

        // todo here catch event from score manager
        public void OnScorePerStepUpdated()
        {
            // take score from event
            // take Roster Cards id
            // check roster score id from data storage
            // update available cards
            // if updated -- send event
        }
    }
}