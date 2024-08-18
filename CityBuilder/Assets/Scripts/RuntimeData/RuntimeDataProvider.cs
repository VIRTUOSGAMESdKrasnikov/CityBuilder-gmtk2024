using CityBuilder.DataStorage;
using CityBuilder.Interfaces;
using UnityEngine;

namespace CityBuilder.RuntimeData
{
    public class RuntimeDataProvider : MonoBehaviour, IRuntimeDataProvider
    {
        [SerializeField] private SpawnableDataStorage _buildingsStorage;
        [SerializeField] private ModelDataStorage _modelsStorage;
        [SerializeField] private UiCardsStorage _uiCardStorage;
        [SerializeField] private UiSoundsStorage _uiSoundsStorage;
        [SerializeField] private OrbitalCameraStorage _orbitalCameraStorage;

        public SpawnableDataStorage BuildingsStorage => _buildingsStorage;
        public ModelDataStorage ModelStorage => _modelsStorage;
        public UiCardsStorage UiCardsStorage => _uiCardStorage;
        public UiSoundsStorage UiSoundsStorage => _uiSoundsStorage;
        public OrbitalCameraStorage OrbitalCameraStorage => _orbitalCameraStorage;
    }
}