using System;
using CityBuilder.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CityBuilder.Game
{
    public class StepManager : MonoBehaviour
    {
        public static event Action Stepped;

        [Inject] private IRuntimeDataProvider _runtimeDataProvider;

        private bool _isCounting;
        
        private void Start()
        {
            StartCounting();
        }

        private void OnDestroy()
        {
            StopCounting();
        }

        public void StartCounting()
        {
            _isCounting = true;
            CountingAsync();
        }
        
        public void StopCounting()
        {
            _isCounting = false;
        }

        private async void CountingAsync()
        {
            while (_isCounting)
            {
                await UniTask.WaitForSeconds(_runtimeDataProvider.RulesStorage.StepDurationInSeconds);
                Stepped?.Invoke();
            }
        }
    }
}