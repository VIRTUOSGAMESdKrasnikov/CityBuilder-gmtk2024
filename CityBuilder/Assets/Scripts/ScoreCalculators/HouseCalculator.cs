using System;
using UnityEngine;

namespace CityBuilder.ScoreCalculators
{
    [CreateAssetMenu(fileName = "HouseCalculator", menuName = "Calculators/HouseCalculator", order = 0)]
    public class HouseCalculator : ScoreCalculatorBase
    {
        protected override int EvaluateObject(Collider collider)
        {
            throw new NotImplementedException("this shouldn't be called at all");
        }
    }
}