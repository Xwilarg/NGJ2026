using NGJ2026.Insect;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace NGJ2026.Manager
{
    public class InsectManager : MonoBehaviour
    {
        public static InsectManager Instance { private set; get; }

        [SerializeField]
        private GameObject _butterflyPrefab;

        public int ButterflyCaught { private set; get; }
        public int BeeCaught { private set; get; }
        public UnityEvent<Butterfly> OnInsectCaught { get; } = new();

        private readonly List<Butterfly> _insects = new();
        public IEnumerable<Butterfly> Insects => _insects;

        private IEnumerable<Transform> _flowers;

        public IEnumerable<Transform> GetAllFlowers() => _flowers;
        public IEnumerable<Transform> GetPossibleFlowers(Vector2 myPos) // TODO: Doesn't seem to work properly (return all) but not prioritary
        {
            var minPlayerDist = GameManager.Instance.Info.MinDistanceWithPlayer;
            return _flowers.Where(f => IsRayInterceptingCircle(myPos, new Vector2(f.position.x, f.position.z), minPlayerDist));
        }

        public void CatchButterfly(Butterfly butterfly)
        {
            ButterflyCaught++;
            OnInsectCaught.Invoke(butterfly);

            _insects.Remove(butterfly);
            Destroy(butterfly.gameObject);
            SpawnButterfly();
        }

        // https://stackoverflow.com/a/76246428
        private bool IsRayInterceptingCircle(Vector2 p1, Vector2 p2, float r)
        {
            float dx = p2.x - p1.x;
            float dy = p2.y - p1.y;
            float u = Mathf.Min(1f, Mathf.Max(0f, ((0f - p1.x) * dx + (0f - p1.y) * dy) / (dy * dy + dx * dx)));
            float nx = p1.x + dx * u - 0f;
            float ny = p1.y + dy * u - 0f;
            return nx * nx + ny * ny < r * r;
        }

        private void Awake()
        {
            Instance = this;

            _flowers = GameObject.FindGameObjectsWithTag("Flower").Select(x => x.transform).ToList();
            Assert.IsTrue(_flowers.Any(), "Couldn't find any flower! Please ensure flowers have the \"Flower\" tag");
        }

        private void Start()
        {
            SpawnButterfly();
        }

        private void SpawnButterfly()
        {
            var go = Instantiate(_butterflyPrefab);
            _insects.Add(go.GetComponent<Butterfly>());

            go.transform.position = LevelManager.Instance.GetRandomSpawnPoint().position;
        }
    }
}
