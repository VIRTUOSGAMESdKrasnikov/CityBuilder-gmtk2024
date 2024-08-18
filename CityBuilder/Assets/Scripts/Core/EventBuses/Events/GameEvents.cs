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

    public class MouseOverCardEvent : IEvent
    {
        public MouseOverCardEvent(){}
    }
    
    public class MouseLeftCardEvent : IEvent
    {
        public MouseLeftCardEvent(){}
    }

    public class DevPanelIgnoreScore : IEvent
    {
        public bool Ignore { get; private set; }

        public DevPanelIgnoreScore(bool ignore)
        {
            Ignore = ignore;
        }
    }

    public class LeftBuildingMode : IEvent
    {
        public LeftBuildingMode(){}
    }
}