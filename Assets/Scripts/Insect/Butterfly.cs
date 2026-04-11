using NGJ2026.Manager;
using Sketch.Common;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NGJ2026.Insect
{
    public class Butterfly : MonoBehaviour
    {
        [SerializeField]
        private GameObject _catchVfx;

        private Vector3 _startPos;

        private Timer _behaviorTimer;
        public Flower TargetFlower { private set; get; }

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
            IEnumerable<Flower> possibles;

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
            if (TargetFlower != null) TargetFlower.Occupant = null;
            TargetFlower = arr[Random.Range(0, arr.Length)];
            TargetFlower.Occupant = this;

            _startPos = transform.position;
            _behaviorTimer.Start(Vector3.Distance(_startPos, TargetFlower.Top.position) * GameManager.Instance.Info.FlyingSpeed);
        }

        public void Catch()
        {
            _catchVfx.SetActive(true);
            Destroy(gameObject, .5f);
        }

        private void Update()
        {
            _behaviorTimer.Update(Time.deltaTime);

            if (State == BehaviorState.Resting) return;

            transform.position = Vector3.Slerp(_startPos, TargetFlower.Top.position, _behaviorTimer.TimerClamped01);
        }

        private void OnDrawGizmos()
        {
            if (TargetFlower == null) return;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, TargetFlower.Top.position);
        }
    }

    public enum BehaviorState
    {
        Flying,
        Resting
    }
}
