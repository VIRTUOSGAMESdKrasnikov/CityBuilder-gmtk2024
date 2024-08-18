namespace CityBuilder.Interfaces
{
    public interface ICollectable
    {
        public int Id { get; }
        public bool IsTaken { get; set; }
        public int ScorePerStep { get; }

        public void ChangeView(bool isInBuildingMode);
    }
}