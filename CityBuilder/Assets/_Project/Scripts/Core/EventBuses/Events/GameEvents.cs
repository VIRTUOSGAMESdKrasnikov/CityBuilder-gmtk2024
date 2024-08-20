using CityBuilder.Spawnables.Scene;

namespace CityBuilder.Core.EventBuses.Events
{
    public class CardClickedEvent : IEvent
    {
        public int ClickedId { get; private set; }

        public CardClickedEvent(int clickedId)
        {
            ClickedId = clickedId;
        }
    }

    public class ScoreChangedEvent : IEvent
    {
        public int CurrentScore { get; private set; }

        public ScoreChangedEvent(int currentScore)
        {
            CurrentScore = currentScore;
        }
    }
    
    public class ScorePerStepChangedEvent : IEvent
    {
        public int CurrentScorePerStep { get; private set; }

        public ScorePerStepChangedEvent(int currentScorePerStep)
        {
            CurrentScorePerStep = currentScorePerStep;
        }
    }

    public class MouseOverCardEvent : IEvent
    {
        public MouseOverCardEvent()
        {
        }
    }

    public class MouseLeftCardEvent : IEvent
    {
        public MouseLeftCardEvent()
        {
        }
    }

    public class DevPanelIgnoreScore : IEvent
    {
        public bool Ignore { get; private set; }

        public DevPanelIgnoreScore(bool ignore)
        {
            Ignore = ignore;
        }
    }

    public class EnteredBuildingMode : IEvent
    {
        public BuildingSpawnable BuildingSpawnable { get; private set; }
        public EnteredBuildingMode(BuildingSpawnable buildingSpawnable)
        {
            BuildingSpawnable = buildingSpawnable;
        }
    }

    public class LeftBuildingMode : IEvent
    {
        public LeftBuildingMode()
        {
        }
    }

    public class PlacedBuildingEvent : IEvent
    {
        public BuildingSpawnable BuildingSpawnable { get; private set; }

        public PlacedBuildingEvent(BuildingSpawnable buildingSpawnable)
        {
            BuildingSpawnable = buildingSpawnable;
        }
    }
}