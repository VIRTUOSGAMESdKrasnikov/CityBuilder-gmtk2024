using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CityBuilder.Game.Ui.CardsVisuals
{
    public class SelectableCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] private CardTurnController _cardTurnController;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(Vector3.one * 1.2f, 0.5f);
            _cardTurnController.OpenFrontFace();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(Vector3.one, 0.5f);
            _cardTurnController.OpenBackFace();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
        }
    }
}