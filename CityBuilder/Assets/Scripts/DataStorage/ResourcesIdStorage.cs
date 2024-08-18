using UnityEngine;

namespace CityBuilder.DataStorage
{
    [CreateAssetMenu(fileName = "ResourcesIdStorage", menuName = "Config/ResourcesIdStorage", order = 0)]
    public class ResourcesIdStorage : ScriptableObject
    {
        [field:SerializeField] public int WoodId { get; private set; } 
        [field:SerializeField] public int StoneId { get; private set; }
    }
}