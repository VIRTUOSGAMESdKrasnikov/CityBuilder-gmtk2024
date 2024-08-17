using CityBuilder.DataStorage.Storageables;
using UnityEngine;

namespace CityBuilder.DataStorage
{
    [CreateAssetMenu(fileName = "UiCardsStorage", menuName = "Config/UiCardsStorage", order = 0)]
    public class UiCardsStorage : DataStorageBase<UiCardStorageable>
    {
    }
}