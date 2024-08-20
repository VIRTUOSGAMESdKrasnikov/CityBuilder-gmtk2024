using CityBuilder.Interfaces;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
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
        [SerializeField] private TextMeshPro _contextTooltip;

        private bool _isTaken;
        
        public int Id => _id;

        public bool IsTaken
        {
            get => _isTaken;
            set
            {
                _isTaken = value;
                if (value)
                {
                    _contextTooltip.gameObject.SetActive(true);
                    _contextTooltip.transform.DOMoveY(transform.position.y + 2f, 2f);
                    _contextTooltip.DOColor(Color.clear, 2f);
                }   
            }
        }

        public int ScorePerStep => _scorePerStep;

        public void ChangeView(bool isInBuildingMode)
        {
            if (isInBuildingMode)
            {
                if (IsTaken)
                    foreach (var renderer in _renderers)
                        renderer.material = _takenBuildingModeMaterial;
                else
                    foreach (var renderer in _renderers)
                        renderer.material = _availableBuildingModeMaterial;
            }
            else
                foreach (var renderer in _renderers)
                    renderer.material = _defaultMaterial;
        }

        [Button]
        private void AutoFindChildRenderers() => _renderers = GetComponentsInChildren<MeshRenderer>();
    }
}