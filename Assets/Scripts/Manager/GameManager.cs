using NGJ2026.SO;
using UnityEngine;

namespace NGJ2026.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { private set; get; }

        [SerializeField]
        private GameInfo _info;
        public GameInfo Info => _info;

        private void Awake()
        {
            Instance = this;
        }
    }
}
