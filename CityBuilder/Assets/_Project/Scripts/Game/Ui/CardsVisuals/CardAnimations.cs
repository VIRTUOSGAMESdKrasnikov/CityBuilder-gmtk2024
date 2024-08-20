using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Events;
using CityBuilder.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace CityBuilder.Game.Ui.CardsVisuals
{
    public class CardAnimations : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Inject] private IRuntimeDataProvider _runtimeDataProvider;

        public void OnPointerEnter(PointerEventData eventData)
        {
            var @event = new MouseOverCardEvent();
            EventBus<MouseOverCardEvent>.Publish(@event);

            transform.DOScale(Vector3.one * 1.2f, 0.5f);

            var cursorData = _runtimeDataProvider.CursorStates.CardHoveredCursor;
            SetCursor(cursorData.Cursor, cursorData.Hotspot);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            var @event = new MouseLeftCardEvent();
            EventBus<MouseLeftCardEvent>.Publish(@event);

            transform.DOScale(Vector3.one, 0.5f);

            var cursorData = _runtimeDataProvider.CursorStates.DefaultCursor;
            SetCursor(cursorData.Cursor, cursorData.Hotspot);
        }

        private void SetCursor(Texture2D cursorTexture, Vector2 hotSpot)
        {
#if UNITY_WEBGL
            Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.ForceSoftware);
#else
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
#endif
        }
    }
}