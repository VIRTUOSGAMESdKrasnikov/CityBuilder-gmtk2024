using UnityEngine;

namespace CityBuilder.DataStorage.Storageables
{
    [System.Serializable]
    public class CursorData
    {
        [field:SerializeField] public Texture2D Cursor { get; private set; }
        [field:SerializeField] public Vector2 Hotspot { get; private set; }
    }
}