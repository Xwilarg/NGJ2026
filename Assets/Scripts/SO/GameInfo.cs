using UnityEngine;

namespace NGJ2026.SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/GameInfo", fileName = "GameInfo")]
    public class GameInfo : ScriptableObject
    {
        [Header("Game")]
        public float GameDuration;
        public Level[] Levels;

        [Header("Insects")]
        public float MinDistanceWithPlayer;
        public Range<float> DelayBeforeInsectRestEnd;
        public float FlyingSpeed;
        public float WingMoveSpeed;
    }

    [System.Serializable]
    public record Range<T>
    {
        public T Min, Max;
    }

    [System.Serializable]
    public record Level
    {
        public int ButterflyCount;
        public bool IsStatic;
        public bool IsSpawningContinue;
    }
}