using UnityEngine;

namespace NGJ2026.Manager
{
    public class DebugManager : MonoBehaviour
    {
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
        }
    }
}
