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
        [SerializeField]
        private AudioSource _sfxNet;
        private float _netStartingVolume;
        private float _maxSfxVelocity = 2.5f;
        [SerializeField]
        private AudioSource _sfxCatchButterfly;
        [SerializeField]
        private float _minSfxPitch = 1f;
        [SerializeField]
        private float _maxSfxPitch = 1f;

        private readonly List<FloatDT> _velocities = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Butterfly>(out var b))
            {
                InsectManager.Instance.CatchButterfly(b);
                _sfxCatchButterfly.Play();
            }
        }

        private void Awake()
        {
            _lastRefPoint = _velRefPoint.position;

            _netStartingVolume = _sfxNet.volume;
        }

        private void Update()
        {
            _velocities.Add(new() { Time = Time.unscaledTime, Value = (_velRefPoint.position - _lastRefPoint).magnitude });
            _lastRefPoint = _velRefPoint.position;

            _velocities.RemoveAll(x => Time.unscaledTime - x.Time > 1f);

            if(_sfxNet)
            {
                float sfxVelocity = Mathf.Clamp01(GetSumVelocity() / _maxSfxVelocity);
                _sfxNet.volume = _netStartingVolume * sfxVelocity;
                _sfxNet.pitch = Mathf.Lerp(_minSfxPitch, _maxSfxPitch, sfxVelocity);
            }
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