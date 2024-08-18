using CityBuilder.Game.Deck;
using UnityEngine;

namespace CityBuilder.DataStorage
{
    [CreateAssetMenu(fileName = "BuildingsDataStorage", menuName = "Config/BuildingsDataStorage", order = 0)]
    public class BuildingsDataStorage : DataStorageBase<DeckItem>
    {
    }
}