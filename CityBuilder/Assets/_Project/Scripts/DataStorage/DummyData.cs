using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.DataStorage
{
    [CreateAssetMenu(fileName = "DummyData", menuName = "Config/DummyData", order = 0)]
    public class DummyData : ScriptableObject
    {
        [field:SerializeField] public List<int> DefaultIds { get; private set; }
    }
}