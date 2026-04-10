using UnityEngine;

namespace NGJ2026.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/GameInfo", fileName = "GameInfo")]
    public class GameInfo : ScriptableObject
    {
        public Range<float> DistanceFromPlayer;
    }

    [System.Serializable]
    public record Range<T>
    {
        public T Min, Max;
    }
}