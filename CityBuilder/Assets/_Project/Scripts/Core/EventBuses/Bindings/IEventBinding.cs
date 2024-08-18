using System;

namespace CityBuilder.Core.EventBuses.Bindings
{
    public interface IEventBinding<T>
    {
        public Action<T> OnEvent { get; set; }
        public Action OnEventNoArgs { get; set; }
    }
}