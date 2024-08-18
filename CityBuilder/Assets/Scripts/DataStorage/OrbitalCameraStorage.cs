using UnityEngine;

namespace CityBuilder.DataStorage
{
    [CreateAssetMenu(fileName = "OrbitalCameraStorage", menuName = "Config/OrbitalCameraStorage", order = 0)]
    public class OrbitalCameraStorage : ScriptableObject
    {
        [field: SerializeField] public int ID { get; set; }

        [field: SerializeField] public float MovementKeyboardSpeed { get; private set; } = 20f;
        [field: SerializeField] public float MovementMouseSensitivity { get; private set; } = .3f;
        [field: SerializeField] public float MovementSmoothness { get; private set; } = 50f;

        [field: SerializeField] public float RotationKeyboardSpeed { get; private set; } = 70f;
        [field: SerializeField] public float RotationMouseSensitivity { get; private set; } = .15f;
        [field: SerializeField] public float RotationSmoothness { get; private set; } = 30f;

        [field: SerializeField] public float ZoomSmoothness { get; private set; } = 50f;
        [field: SerializeField] public float MaxZoomSize { get; private set; } = 30f;
        [field: SerializeField] public float MinZoomSize { get; private set; } = 5f;
        [field: SerializeField] public float ZoomStepsCount { get; private set; } = 20f;
    }
}