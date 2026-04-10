using NGJ2026.SO;
using UnityEngine;

namespace NGJ2026.Manager
{
    public class DebugManager : MonoBehaviour
    {
        [SerializeField]
        private bool _disableDebug;

        [SerializeField]
        private GameObject _debugUI;

        [SerializeField]
        private GameInfo _info;

        private void Awake()
        {
            if (
#if UNITY_EDITOR
                _disableDebug
#else
                true
#endif
                )
            {
                _debugUI.SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(Vector3.up, new(1f, 2f, 1f));

            if (InsectManager.Instance != null)
            {
                foreach (var insect in InsectManager.Instance.Insects)
                {
                    Gizmos.color = insect.State == Insect.BehaviorState.Resting ? Color.red : Color.blue;
                    Gizmos.DrawSphere(insect.transform.position, insect.transform.localScale.x);
                }
            }

            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(Vector3.zero, _info.MinDistanceWithPlayer);
        }
    }
}
