using UnityEngine;

namespace CityBuilder.DataStorage
{
    [CreateAssetMenu(fileName = "GameRulesStorage", menuName = "Config/GameRulesStorage", order = 0)]
    public class GameRulesStorage : ScriptableObject
    {
        [field: SerializeField] public float StepDurationInSeconds { get; private set; } = 5f;
    }
}