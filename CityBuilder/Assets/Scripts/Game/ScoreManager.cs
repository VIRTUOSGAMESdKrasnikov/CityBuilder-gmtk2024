using CityBuilder.Core.EventBuses;
using CityBuilder.Core.EventBuses.Events;
using UnityEngine;

namespace CityBuilder.Game
{
    public class ScoreManager : MonoBehaviour
    {
        public int ScoreCount { get; private set; }

        public void AddScore(int count)
        {
            ScoreCount += count;
            if (ScoreCount <= 0)
                ScoreCount = 0;

            var @event = new ScoreChangedEvent(ScoreCount);
            EventBus<ScoreChangedEvent>.Publish(@event);
        }
    }
}