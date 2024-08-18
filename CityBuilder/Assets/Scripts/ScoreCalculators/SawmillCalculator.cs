using CityBuilder.Interfaces;
using UnityEngine;
using Zenject;

namespace CityBuilder.ScoreCalculators
{
    [CreateAssetMenu(fileName = "SawmillCalculator", menuName = "Calculators/SawmillCalculator", order = 0)]
    public class SawmillCalculator : ScoreCalculatorBase
    {
        [Inject] private IRuntimeDataProvider _runtimeDataProvider;

        protected override int EvaluateObject(Collider collider)
        {
            Debug.Log($"validating item {collider.name}");
            
            if (collider.TryGetComponent<ICollectable>(out var collectable))
            {
                if (collectable.Id == _targetResourceId && !collectable.IsTaken)
                {
                    return 1;
                }
            }

            return 0;
        }
    }
}