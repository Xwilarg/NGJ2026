using NGJ2026.Manager;
using Sketch.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NGJ2026.Insect
{
    public class Butterfly : MonoBehaviour
    {
        private Vector3 _startPos;
        private Vector3 _target;

        private Timer _behaviorTimer;

        private BehaviorState _state = BehaviorState.Flying;
        public BehaviorState State
        {
            private set
            {
                _state = value;

                if (State == BehaviorState.Flying)
                {
                    GetTarget(true);
                }
                else
                {
                    if (!GameManager.Instance.CurrentLevel.IsStatic)
                        _behaviorTimer.Start(Random.Range(GameManager.Instance.Info.DelayBeforeInsectRestEnd.Min, GameManager.Instance.Info.DelayBeforeInsectRestEnd.Max));
                }
            }
            get => _state;
        }

        private void Awake()
        {
            _behaviorTimer = new();

            _behaviorTimer.OnDone.AddListener(() =>
            {
                if (State == BehaviorState.Resting) State = BehaviorState.Flying;
                else State = BehaviorState.Resting;
            });
        }

        private void Start()
        {
            GetTarget(false);
        }

        public void GetTarget(bool ignorePlayer)
        {
            IEnumerable<Transform> possibles;

            if (ignorePlayer)
            {
                possibles = InsectManager.Instance.GetAllFlowers();
            }
            else
            {
                possibles = InsectManager.Instance.GetPossibleFlowers(new Vector2(transform.position.x, transform.position.z));

                if (!possibles.Any())
                {
                    Debug.LogWarning("Butterfly can't find a flower, falling back on ignoring player");
                    GetTarget(true);
                    return;
                }
            }

            var arr = possibles.ToArray();
            _target = arr[Random.Range(0, arr.Length)].position;

            _startPos = transform.position;
            _behaviorTimer.Start(Vector3.Distance(_startPos, _target) * GameManager.Instance.Info.FlyingSpeed);
        }

        private void Update()
        {
            _behaviorTimer.Update(Time.deltaTime);

            if (State == BehaviorState.Resting) return;

            transform.position = Vector3.Slerp(_startPos, _target, _behaviorTimer.TimerClamped01);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, _target);
        }
    }

    public enum BehaviorState
    {
        Flying,
        Resting
    }
}
