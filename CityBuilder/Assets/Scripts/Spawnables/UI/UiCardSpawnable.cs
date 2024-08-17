using CityBuilder.Game.Ui.CardsVisuals;
using CityBuilder.Interfaces;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CityBuilder.Spawnables.UI
{
    public class UiCardSpawnable : UiSpawnable
    {
        [Inject] private IRuntimeDataProvider _runtimeDataProvider;
        [SerializeField] private CardTurnController _cardTurnController;
        
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        
        public override async UniTask<bool> Spawn(int id)
        {
            _cardTurnController.OpenBackFace(true);
            return true;
        }
    }
}