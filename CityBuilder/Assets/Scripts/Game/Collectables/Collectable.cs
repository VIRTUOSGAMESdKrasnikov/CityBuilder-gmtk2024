using CityBuilder.Interfaces;
using UnityEngine;

namespace CityBuilder.Game.Collectables
{
    public class Collectable : MonoBehaviour, ICollectable
    {
        [SerializeField] private int _id;
        [SerializeField] private int _scorePerStep;

        public int Id => _id;
        public bool IsTaken { get; set; }
        public int ScorePerStep => _scorePerStep;
    }
}