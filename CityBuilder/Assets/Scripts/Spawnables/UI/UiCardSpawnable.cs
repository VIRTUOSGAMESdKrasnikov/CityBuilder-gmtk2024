using System;
using CityBuilder.Game.Ui.CardsVisuals;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CityBuilder.Spawnables.UI
{
    public class UiCardSpawnable : UiSpawnable
    {
        public int Id { get; private set; }
        
        [SerializeField] private CardTurnController _cardTurnController;
        [SerializeField] private SelectableCard _selectableCard;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;

        public override async UniTask<bool> Spawn(int id)
        {
            Id = id;
            
            _cardTurnController.OpenBackFace(true);
            _selectableCard.SetId(id);

            return true;
        }

        public void SetInteractable(bool interactable)
        {
            _canvasGroup.blocksRaycasts = interactable;
        }
    }
}