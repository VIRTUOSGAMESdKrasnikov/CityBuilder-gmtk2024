using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.ScoreCalculators
{
    public abstract class ScoreCalculatorBase : ScriptableObject
    {
        [SerializeField] protected int _targetResourceId;
        
        public int GetScore(Transform transform, float range)
        {
            return GetScore(Physics.OverlapBox(transform.position, new Vector3(range / 2, 100, range / 2), Quaternion.identity, LayerMask.GetMask("Collectable")));
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