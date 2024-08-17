using System.Collections.Generic;
using System.Linq;
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
            _gameManager.OnUiCardClicked(id);
            Debug.Log($"card with id {id} clicked");
        }
    }
}