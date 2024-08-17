using DG.Tweening;
using UnityEngine;

namespace CityBuilder.Game.Ui.CardsVisuals
{
    public class CardTurnController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _front;
        [SerializeField] private CanvasGroup _back;

        private bool _backFaceUp = true;
        
        public void OpenBackFace(bool force = false)
        {
            if (_backFaceUp)
            {
                return;
            }
            
            _backFaceUp = true;

            StartCardTurningAnimation(force);
        }

        public void OpenFrontFace(bool force = false)
        {
            if (!_backFaceUp)
            {
                return;
            }
            
            _backFaceUp = false;
            
            StartCardTurningAnimation(force);
        }

        public void TurnCard()
        {
            if (_backFaceUp)
            {
                OpenFrontFace();
            }
            else
            {
                OpenBackFace();
            }
        }

        private void StartCardTurningAnimation(bool force)
        {
            _front.gameObject.SetActive(true);
            _back.gameObject.SetActive(true);
            
            float switchDuration = force ? 0 : 0.5f;

            if (_backFaceUp)
            {
                _front.alpha = 1;
                _back.alpha = 0;

                _front.DOFade(0, switchDuration).onComplete += () =>
                {
                    _front.gameObject.SetActive(!_backFaceUp);
                    _back.gameObject.SetActive(_backFaceUp);
                };
                _back.DOFade(1, switchDuration);
            }
            else
            {
                _front.alpha = 0;
                _back.alpha = 1;

                _front.DOFade(1, switchDuration).onComplete += () =>
                {
                    _front.gameObject.SetActive(!_backFaceUp);
                    _back.gameObject.SetActive(_backFaceUp);
                };
                _back.DOFade(0, switchDuration);
            }
        }
    }
}