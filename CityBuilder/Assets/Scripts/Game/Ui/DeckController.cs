using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CityBuilder.DataStorage;
using CityBuilder.DataStorage.Storageables;
using CityBuilder.Game.Deck;
using CityBuilder.Interfaces;
using CityBuilder.Spawnables.UI;
using CityBuilder.Spawners;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CityBuilder.Game.Ui
{
    public class DeckController : MonoBehaviour
    {
        [SerializeField] private Transform _activeCardsParent;

        [Inject] private DiContainer _container;
        [Inject] private IRuntimeDataProvider _runtimeDataProvider;

        private ISpawner<UiCardsStorage, UiCardStorageable, UiCardSpawnable> _cardsSpawner;

        private PlayerDeck _playerDeck;
        private IEnumerable<UiCardSpawnable> _spawnedCards;
        
        private void Awake()
        {
            _cardsSpawner = new UiCardsSpawner();
            _cardsSpawner.Init(_runtimeDataProvider.UiCardsStorage, _container);
        }
        
        private async void Start()
        {
            await SpawnCards();
        }

        private async UniTask SpawnCards()
        {
            _spawnedCards = await _cardsSpawner.Spawn(_playerDeck.RosterCards);

            foreach (var card in _spawnedCards)
            {
                card.transform.SetParent(_activeCardsParent);

                card.transform.localPosition = Vector3.zero;
                card.transform.localScale = Vector3.one;
                card.transform.localRotation = Quaternion.identity;
            }
            
            // todo add sth like "disable card input"
            // like spawn all cards, find diff between RosterCards and available
            // and then turn that diff closed side so user cant interact with them 
        }

        public void SetCardsRoster(PlayerDeck cardsRoster)
        {
            _playerDeck = cardsRoster;
            _playerDeck.AvailableCardsUpdated += OnDeckUpdated;
        }

        private async void OnDeckUpdated()
        {
            await SpawnCards();
        }
    }
}