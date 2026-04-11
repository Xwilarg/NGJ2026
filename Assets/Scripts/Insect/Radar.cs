using NGJ2026.Player;
using UnityEngine;

namespace NGJ2026.Insect
{
    public class Radar : MonoBehaviour
    {
        [SerializeField]
        private Butterfly _owner;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Net>(out var net))
            {
                Debug.Log($"[VEL] Net velocity is at {net.GetSumVelocity()}");
            }
        }
    }
}
