using System.Collections.Generic;
using CityBuilder.Core.EventBuses.Bindings;
using CityBuilder.Core.EventBuses.Events;

namespace CityBuilder.Core.EventBuses
{
    public static class EventBus<T> where T : IEvent
    {
        private static readonly HashSet<IEventBinding<T>> _eventBindings = new HashSet<IEventBinding<T>>();

        public static void Subscribe(IEventBinding<T> binding)
        {
            _eventBindings.Add(binding);
        }

        public static void Unsubscribe(IEventBinding<T> binding)
        {
            _eventBindings.Remove(binding);
        }

        public static void Publish(T @event)
        {
            foreach (var binding in _eventBindings)
            {
                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();
            }
        }
    }
}