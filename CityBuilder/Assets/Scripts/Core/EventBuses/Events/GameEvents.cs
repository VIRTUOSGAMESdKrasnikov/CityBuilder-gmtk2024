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
}