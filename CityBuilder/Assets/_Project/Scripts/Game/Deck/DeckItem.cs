using CityBuilder.Interfaces;
using UnityEngine;

namespace CityBuilder.Game.Deck
{
    [System.Serializable]
    public class DeckItem : IStorageable
    {
        [field:SerializeField] public int ID { get; set; }
        [field:SerializeField] public bool AvailableOnStart { get; private set; }
        [field:SerializeField] public int ScorePerStepNeeded { get; private set; }
        [field:SerializeField] public int ScorePerStepProduced { get; private set; }
    }
}