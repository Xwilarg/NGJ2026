using UnityEngine;

namespace NGJ2026.Insect
{
    public class Flower : MonoBehaviour
    {
        [SerializeField]
        private Transform _top;
        public Transform Top => _top;
    }
}
