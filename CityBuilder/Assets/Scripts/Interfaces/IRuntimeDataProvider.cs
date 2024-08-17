using CityBuilder.DataStorage;

namespace CityBuilder.Interfaces
{
    public interface IRuntimeDataProvider
    {
        public SpawnableDataStorage UnitStorage { get; }
        public ModelDataStorage UnitsModelStorage { get; }
    }
}