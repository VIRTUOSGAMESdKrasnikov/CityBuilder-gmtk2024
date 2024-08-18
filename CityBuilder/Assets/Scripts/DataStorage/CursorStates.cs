using CityBuilder.DataStorage.Storageables;
using UnityEngine;

namespace CityBuilder.DataStorage
{
    [CreateAssetMenu(fileName = "CursorStates", menuName = "Config/Visuals/CursorStates", order = 0)]
    public class CursorStates : ScriptableObject
    {
        [field:SerializeField] public CursorData DefaultCursor { get; private set; }
        [field:SerializeField] public CursorData CardHoveredCursor { get; private set; }
        [field:SerializeField] public CursorData MovingBuildingCursor { get; private set; }
    }
}