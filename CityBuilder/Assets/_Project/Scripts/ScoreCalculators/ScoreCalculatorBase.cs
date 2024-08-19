using System.Collections.Generic;
using CityBuilder.Core;
using UnityEngine;

namespace CityBuilder.ScoreCalculators
{
    public abstract class ScoreCalculatorBase : ScriptableObject
    {
        [SerializeField] protected int _targetResourceId;

        public int TargetResourceId => _targetResourceId;
        
        public int GetScore(Transform transform, float radius)
        {
            bool hasHouseNear = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask(Constants.HOUSE_LAYER_NAME)).Length > 0;
            if (hasHouseNear)
                return GetScore(Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask(Constants.COLLECTABLE_LAYER_NAME)));

            return 0;
        }

        private int GetScore(IEnumerable<Collider> objects)
        {
            int score = 0;

            foreach (var collider in objects) 
                score += EvaluateObject(collider);

            return score;
        }

        protected abstract int EvaluateObject(Collider collider);
    }
}