using UnityEngine;

namespace NGJ2026.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/GameInfo", fileName = "GameInfo")]
    public class GameInfo : ScriptableObject
    {
        [Header("Game")]
        public float GameDuration;

        [Header("Insects")]
        public float MinDistanceWithPlayer;
        public Range<float> DelayBeforeInsectRestEnd;
        public float FlyingSpeed;
    }

    [System.Serializable]
    public record Range<T>
    {
        public T Min, Max;
    }
}