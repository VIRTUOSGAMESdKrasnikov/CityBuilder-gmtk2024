using CityBuilder.Interfaces;
using UnityEngine;

namespace CityBuilder.Game.Collectables
{
    public class Collectable : MonoBehaviour, ICollectable
    {
        [SerializeField] private int _id;
        [SerializeField] private int _scorePerStep;

        [SerializeField] private MeshRenderer[] _renderers;
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _takenBuildingModeMaterial;
        [SerializeField] private Material _availableBuildingModeMaterial;

        public int Id => _id;
        public bool IsTaken { get; set; }
        public int ScorePerStep => _scorePerStep;

        public void ChangeView(bool isInBuildingMode)
        {
            if (isInBuildingMode)
            {
                if (IsTaken)
                {
                    foreach (var renderer in _renderers)
                    {
                        renderer.material = _takenBuildingModeMaterial;
                    }
                }
                else
                {
                    foreach (var renderer in _renderers)
                    {
                        renderer.material = _availableBuildingModeMaterial;
                    }
                }
            }
            else
            {
                foreach (var renderer in _renderers)
                {
                    renderer.material = _defaultMaterial;
                }
            }
        }

        [ContextMenu("AutoFindChildRenderers")]
        private void AutoFindChildRenderers()
        {
            _renderers = GetComponentsInChildren<MeshRenderer>();
        }
    }
}