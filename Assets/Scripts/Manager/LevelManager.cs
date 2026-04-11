using System.Linq;
using TMPro;
using UnityEngine;

namespace NGJ2026.Manager
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { private set; get; }

        [SerializeField]
        private Transform[] _insectSpawns;

        [SerializeField]
        private TMP_Text[] _tombstoneNames;

        public Transform GetRandomSpawnPoint()
            => _insectSpawns[Random.Range(0, _insectSpawns.Length)];

        private void Awake()
        {
            Instance = this;

            UpdateTombstones();
        }

        private void Start()
        {
            GameManager.Instance.OnGameSetup.AddListener(UpdateTombstones);
        }

        public void UpdateTombstones()
        {
            var names = GetNames().OrderBy(_ => Random.value).ToArray();
            for (int i = 0; i < Mathf.Min(_tombstoneNames.Length, names.Length); i++)
            {
                _tombstoneNames[i].name = names[i];
            }
        }

        public string[] GetNames()
        {
            return new string[]
            {
                Random.Range(0, 10) == 0 ? "Thomas Van 'Boo'wel" : "Thomas Van Bouwel",
                "Ronja Palaszewski",
                "Anouk van Uffelen",
                "Elias Abildgaard",
                "Christian Chaux",
                Random.Range(0, 10) == 0 ? "Jon \"Kill\"iher" : "Jon Kelliher"
            };
        }
    }
}
