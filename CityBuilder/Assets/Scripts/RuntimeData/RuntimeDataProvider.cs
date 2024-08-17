using CityBuilder.DataStorage;
using CityBuilder.Interfaces;
using UnityEngine;
using Zenject;

namespace CityBuilder.RuntimeData
{
    public class RuntimeDataProvider : MonoBehaviour, IRuntimeDataProvider
    {
        [Inject] private SpawnableDataStorage _unitStorage;
        [Inject] private ModelDataStorage _unitsModelsStorage;

        public SpawnableDataStorage UnitStorage => _unitStorage;
        public ModelDataStorage UnitsModelStorage => _unitsModelsStorage;
    }
}