using NGJ2026.Insect;
using NGJ2026.Manager;
using UnityEngine;

namespace NGJ2026.Player
{
    public class Net : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Butterfly>(out var b))
            {
                InsectManager.Instance.CatchButterfly(b);
            }
        }
    }
}