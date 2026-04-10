using NGJ2026.Manager;
using Sketch.Common;
using UnityEngine;

namespace NGJ2026.Insect
{
    public class Butterfly : MonoBehaviour
    {
        private float _timer;

        private float _distanceWithCenter;

        private Timer _behaviorTimer;

        public BehaviorState State { private set; get; } = BehaviorState.Flying;

        private void Awake()
        {
            _behaviorTimer = new();

            _behaviorTimer.OnDone.AddListener(() =>
            {
                if (State == BehaviorState.Resting) State = BehaviorState.Flying;
                else State = BehaviorState.Resting;

                ResetTimer();
            });
            ResetTimer();
        }

        private void Start()
        {
            // TODO: Calculate base _timer depending of position
            _distanceWithCenter = new Vector2(transform.position.x, transform.position.z).magnitude;
        }

        public void ResetTimer()
        {
            _behaviorTimer.Start(
               State == BehaviorState.Flying
                ? Random.Range(GameManager.Instance.Info.DelayBeforeInsectRestStart.Min, GameManager.Instance.Info.DelayBeforeInsectRestStart.Max)
                : Random.Range(GameManager.Instance.Info.DelayBeforeInsectRestEnd.Min, GameManager.Instance.Info.DelayBeforeInsectRestEnd.Max)
            );
        }

        private void Update()
        {
            _behaviorTimer.Update(Time.deltaTime);

            if (State == BehaviorState.Resting) return;

            _timer += Time.deltaTime;

            transform.position = new(
                x: Mathf.Cos(_timer) * _distanceWithCenter,
                y: 1f + Mathf.Sin(_timer),
                z: Mathf.Sin(_timer) * _distanceWithCenter
            );
        }
    }

    public enum BehaviorState
    {
        Flying,
        Resting
    }
}
