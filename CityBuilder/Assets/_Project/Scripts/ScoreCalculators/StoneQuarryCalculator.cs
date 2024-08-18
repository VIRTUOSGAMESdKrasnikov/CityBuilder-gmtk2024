using CityBuilder.Interfaces;
using UnityEngine;

namespace CityBuilder.ScoreCalculators
{
    [CreateAssetMenu(fileName = "StoneQuarryCalculator", menuName = "Calculators/StoneQuarryCalculator", order = 0)]
    public class StoneQuarryCalculator : ScoreCalculatorBase
    {
        protected override int EvaluateObject(Collider collider)
        {
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