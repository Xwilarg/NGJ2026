using NGJ2026.Insect;
using NGJ2026.Manager;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NGJ2026.Player
{
    public class Net : MonoBehaviour
    {
        private Vector3 _lastRefPoint;
        [SerializeField]
        private Transform _velRefPoint;

        private readonly List<FloatDT> _velocities = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Butterfly>(out var b))
            {
                InsectManager.Instance.CatchButterfly(b);
            }
        }

        private void Awake()
        {
            _lastRefPoint = _velRefPoint.position;
        }

        private void Update()
        {
            _velocities.Add(new() { Time = Time.unscaledTime, Value = (_velRefPoint.position - _lastRefPoint).magnitude });
            _lastRefPoint = _velRefPoint.position;

            _velocities.RemoveAll(x => Time.unscaledTime - x.Time > 1f);
        }

        public float GetSumVelocity()
        {
            return _velocities.Sum(x => x.Value);
        }
    }

    public record FloatDT
    {
        public float Time;
        public float Value;
    }
}