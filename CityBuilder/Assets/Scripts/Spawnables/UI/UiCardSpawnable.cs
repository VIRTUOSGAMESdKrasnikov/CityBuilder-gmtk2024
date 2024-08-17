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

        [SerializeField] private GameObject _front;
        [SerializeField] private GameObject _back;
        
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        
        public override async UniTask<bool> Spawn(int id)
        {
            _front.SetActive(false);
            _back.SetActive(true);
            return true;
        }
    }
}