using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CityBuilder.Spawnables.Scene
{
    public class BuildingModelSpawnable : SceneSpawnable
    {
        [SerializeField] private MeshRenderer[] _modelRenderers;
        
        [SerializeField] private Material _originalMaterial;
        [SerializeField] private Material _ghostMaterialCanBePlaced;
        [SerializeField] private Material _ghostMaterialPlacingForbidden;
        
        public override UniTask<bool> Spawn(int id)
        {
            throw new System.NotImplementedException();
        }
        
        public void UpdateModelGhostState(bool isGhost, bool canBePlaced)
        {
            if (isGhost)
            {
                if (canBePlaced)
                {
                    foreach (var renderer in _modelRenderers)
                    {
                        renderer.material = _ghostMaterialCanBePlaced;
                    }
                }
                else
                {
                    foreach (var renderer in _modelRenderers)
                    {
                        renderer.material = _ghostMaterialPlacingForbidden;
                    }
                }
            }
            else
            {
                foreach (var renderer in _modelRenderers)
                {
                    renderer.material = _originalMaterial;
                }
            }
        }
        
        [Button]
        private void AutoFindChildRenderers()
        {
            _modelRenderers = GetComponentsInChildren<MeshRenderer>();
        }
    }
}