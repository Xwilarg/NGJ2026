using UnityEngine;

namespace NGJ2026.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/GameInfo", fileName = "GameInfo")]
    public class GameInfo : ScriptableObject
    {
        [Header("Insects")]
        public float MinDistanceWithPlayer;
        public Range<float> DelayBeforeInsectRestStart;
        public Range<float> DelayBeforeInsectRestEnd;
    }

    [System.Serializable]
    public record Range<T>
    {
        public T Min, Max;
    }
}