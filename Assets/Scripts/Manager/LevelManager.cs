using UnityEngine;

namespace NGJ2026.Manager
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { private set; get; }

        [SerializeField]
        private Transform[] _insectSpawns;

        public Transform GetRandomSpawnPoint()
            => _insectSpawns[Random.Range(0, _insectSpawns.Length)];

        private void Awake()
        {
            Instance = this;
        }
    }
}
