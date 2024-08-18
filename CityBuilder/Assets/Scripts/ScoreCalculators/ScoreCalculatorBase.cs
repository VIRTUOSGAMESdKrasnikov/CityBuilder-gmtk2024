using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.ScoreCalculators
{
    public abstract class ScoreCalculatorBase : ScriptableObject
    {
        [SerializeField] protected float _range;
        [SerializeField] protected int _targetResourceId;
        
        public int GetScore(Transform transform)
        {
            return GetScore(Physics.OverlapBox(transform.position, new Vector3(_range / 2, 100, _range / 2), Quaternion.identity, LayerMask.GetMask("Collectable")));
        }

        private int GetScore(IEnumerable<Collider> objects)
        {
            int score = 0;

            foreach (var collider in objects)
            {
                score += EvaluateObject(collider);
            }

            return score;
        }

        protected abstract int EvaluateObject(Collider collider);
    }
}