namespace CityBuilder.Interfaces
{
    public interface ICollectable
    {
        public int Id { get; }
        public bool IsTaken { get; }
        public int ScorePerStep { get; }
    }
}