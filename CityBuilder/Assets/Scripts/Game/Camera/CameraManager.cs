using Cinemachine;
using UnityEngine;

namespace CityBuilder.Game.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Transform cameraRig;
        [SerializeField] private Transform cameraTrans;
        private ICameraController _currentCameraController;

        private void Awake() =>
            _currentCameraController = new OrbitalCameraController(cameraRig, cameraTrans);

        private void OnEnable() => _currentCameraController.Init();
        private void OnDisable() => _currentCameraController.Dispose();
        private void Update() => _currentCameraController.Tick();
    }
}