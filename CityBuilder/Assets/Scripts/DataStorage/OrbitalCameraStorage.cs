using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace CityBuilder.DataStorage
{
    [CreateAssetMenu(fileName = "OrbitalCameraStorage", menuName = "Config/OrbitalCameraStorage", order = 0)]
    public class OrbitalCameraStorage : ScriptableObject
    {
        [TitleGroup("Movement")] [SerializeField]
        private float movementKeyboardSpeed = 20f;

        [SerializeField] private float movementMouseSensitivity = .3f;

        [SerializeField] private float movementSmoothness = 50f;

        [TitleGroup("Rotation")] [SerializeField]
        private float rotationKeyboardSpeed = 70f;

        [SerializeField] private float rotationMouseSensitivity = .15f;

        [SerializeField] private float rotationSmoothness = 30f;

        [SerializeField] private float xRotation = 45f;

        [TitleGroup("Zoom")] [SerializeField] private float zoomSmoothness = 50f;

        [SerializeField] private float maxZoomSize = 30f;

        [SerializeField] private float minZoomSize = 5f;

        [SerializeField] private float zoomStepsCount = 20f;

        public float MovementKeyboardSpeed => movementKeyboardSpeed;
        public float MovementMouseSensitivity => movementMouseSensitivity;
        public float MovementSmoothness => movementSmoothness;
        public float RotationKeyboardSpeed => rotationKeyboardSpeed;
        public float RotationMouseSensitivity => rotationMouseSensitivity;
        public float RotationSmoothness => rotationSmoothness;
        public float XRotation => xRotation;
        public float ZoomSmoothness => zoomSmoothness;
        public float MaxZoomSize => maxZoomSize;
        public float MinZoomSize => minZoomSize;
        public float ZoomStepsCount => zoomStepsCount;
    }
}