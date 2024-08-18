using CityBuilder.Interfaces;
using UnityEngine;

namespace CityBuilder.Game.Collectables
{
    public class Collectable : MonoBehaviour, ICollectable
    {
        [SerializeField] private int _id;
        [SerializeField] private int _scorePerStep;

        private bool _isTaken;

        public int Id => _id;
        public bool IsTaken => _isTaken;
        public int ScorePerStep => _scorePerStep;
    }
}