using UnityEngine;

namespace NGJ2026.Player
{
    public class Net : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("butterfly caught");
        }
    }
}