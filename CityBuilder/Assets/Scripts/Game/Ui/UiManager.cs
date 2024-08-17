using System;
using System.Collections.Generic;
using System.Linq;
using CityBuilder.DataStorage;
using UnityEngine;

namespace CityBuilder.Game.Ui
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private DummyData _dummyData;
        [SerializeField] private DeckController _deckController;

        private List<int> _availableCardsIds;
        
        public void Init(IEnumerable<int> availableCardsIds)
        {
            _availableCardsIds = (availableCardsIds == null || !availableCardsIds.Any())
                ? _dummyData.DefaultIds
                : availableCardsIds.ToList();

            _deckController.SetAvailableCards(_availableCardsIds);
            _deckController.CardClicked += OnCardClicked;
        }

        private void OnDestroy()
        {
            _deckController.CardClicked -= OnCardClicked;
        }

        private void OnCardClicked(int id)
        {
            Debug.LogError($"card with id {id} clicked");
        }
    }
}