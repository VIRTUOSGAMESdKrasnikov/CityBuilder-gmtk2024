using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Events;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CityBuilder.Game.Ui.CardsVisuals
{
    public class CardAnimations : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CardTurnController _cardTurnController;

        public void OnPointerEnter(PointerEventData eventData)
        {
            var @event = new MouseOverCardEvent();
            EventBus<MouseOverCardEvent>.Publish(@event);

            transform.DOScale(Vector3.one * 1.2f, 0.5f);
            _cardTurnController.OpenFrontFace();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            var @event = new MouseLeftCardEvent();
            EventBus<MouseLeftCardEvent>.Publish(@event);
            
            transform.DOScale(Vector3.one, 0.5f);
            _cardTurnController.OpenBackFace();
        }
    }
}