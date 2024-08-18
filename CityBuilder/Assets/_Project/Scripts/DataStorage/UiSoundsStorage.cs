using UnityEngine;

namespace CityBuilder.DataStorage
{
    [CreateAssetMenu(fileName = "UiSounds", menuName = "Config/UiSounds", order = 0)]
    public class UiSoundsStorage : ScriptableObject
    {
        [field:SerializeField] public AudioClip[] MouseOverCardSounds { get; private set; }
        [field:SerializeField] public AudioClip[] MouseLeftCardSounds { get; private set; }
        [field:SerializeField] public AudioClip[] BuildingPlacedSounds { get; private set; }
    }
}