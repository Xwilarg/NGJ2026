using UnityEngine;

namespace NGJ2026.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/GameInfo", fileName = "GameInfo")]
    public class GameInfo : ScriptableObject
    {
        [Header("Insects")]
        public Range<float> DistanceFromPlayer;
        public Range<float> DelayBeforeInsectRestStart;
        public Range<float> DelayBeforeInsectRestEnd;
    }

    [System.Serializable]
    public record Range<T>
    {
        public T Min, Max;
    }
}