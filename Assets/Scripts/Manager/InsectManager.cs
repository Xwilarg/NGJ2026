using NGJ2026.Insect;
using System.Collections.Generic;
using UnityEngine;

namespace NGJ2026.Manager
{
    public class InsectManager : MonoBehaviour
    {
        public static InsectManager Instance { private set; get; }

        [SerializeField]
        private GameObject _butterflyPrefab;

        private List<Butterfly> _insects = new();
        public IEnumerable<Butterfly> Insects => _insects;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SpawnButterfly();
        }

        private void SpawnButterfly()
        {
            var go = Instantiate(_butterflyPrefab);
            _insects.Add(go.GetComponent<Butterfly>());

            var randPos = Random.onUnitCircle * Random.Range(GameManager.Instance.Info.DistanceFromPlayer.Min, GameManager.Instance.Info.DistanceFromPlayer.Max);
            go.transform.position = new(randPos.x, 1f, randPos.y);
        }
    }
}
