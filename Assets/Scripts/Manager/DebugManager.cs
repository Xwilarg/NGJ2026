using UnityEngine;

namespace NGJ2026.Manager
{
    public class DebugManager : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(Vector3.up, new(1f, 2f, 1f));
        }
    }
}
