using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CityBuilder.Game.Ui.CardsVisuals
{
    public class SelectableCard : MonoBehaviour, IPointerDownHandler
    {
        public event Action<int> CardClicked;

        private int _id;

        public void SetId(int id)
        {
            _id = id;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            CardClicked?.Invoke(_id);
        }
    }
}