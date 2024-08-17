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
        [SerializeField] private CardTurnController _cardTurnController;
        [SerializeField] private SelectableCard _selectableCard;
        
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;

        public override async UniTask<bool> Spawn(int id)
        {
            _cardTurnController.OpenBackFace(true);
            _selectableCard.SetId(id);

            return true;
        }
    }
}