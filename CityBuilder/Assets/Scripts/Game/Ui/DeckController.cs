using System;
using System.Collections.Generic;
using CityBuilder.DataStorage;
using CityBuilder.DataStorage.Storageables;
using CityBuilder.Interfaces;
using CityBuilder.Spawnables.UI;
using CityBuilder.Spawners;
using UnityEngine;
using Zenject;

namespace CityBuilder.Game.Ui
{
    public class DeckController : MonoBehaviour
    {
        [SerializeField] private Transform _pileParent;
        [SerializeField] private Transform _activeCardsParent;

        [Inject] private DiContainer _container;
        [Inject] private IRuntimeDataProvider _runtimeDataProvider;

        private ISpawner<UiCardsStorage, UiCardStorageable, UiCardSpawnable> _cardsSpawner;

        private List<int> _availableCardsIds;
        private IEnumerable<UiCardSpawnable> _spawnedCards;
        
        private void Awake()
        {
            _cardsSpawner = new UiCardsSpawner();
            _cardsSpawner.Init(_runtimeDataProvider.UiCardsStorage, _container);
        }
        
        private async void Start()
        {
            var spawnedItems = await _cardsSpawner.Spawn(_availableCardsIds);

            // todo add pile position algorithm
            // like cards would be placed with offset
            foreach (var card in spawnedItems)
            {
                card.transform.SetParent(_pileParent);

                card.transform.localPosition = Vector3.zero;
                card.transform.localScale = Vector3.one;
                card.transform.localRotation = Quaternion.identity;

                card.CardClicked += OnCardClicked;
            }
        }

        private void OnDestroy()
        {
            foreach (var card in _spawnedCards)
            {
                card.CardClicked -= OnCardClicked;
            }
        }

        public void SetAvailableCards(List<int> availableCardsIds)
        {
            _availableCardsIds = availableCardsIds;
        }

        private void OnCardClicked(int id)
        {
            Debug.LogError($"card with id {id} clicked");
        }
    }
}