using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CityBuilder.Game.Ui.CardsVisuals
{
    public class SelectableCard : MonoBehaviour, IPointerDownHandler
    {
        private int _id;

        public void SetId(int id)
        {
            _id = id;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            var cardClickedEvent = new CardClickedEvent(_id);
            EventBus<CardClickedEvent>.Publish(cardClickedEvent);
        }
    }
}