using UnityEngine;

namespace NGJ2026.Insect
{
    public class Butterfly : MonoBehaviour
    {
        private float _timer;

        private float _distanceWithCenter;

        private void Start()
        {
            _distanceWithCenter = new Vector2(transform.position.x, transform.position.z).magnitude;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            transform.position = new(
                x: Mathf.Cos(_timer) * _distanceWithCenter,
                y: 1f + Mathf.Sin(_timer),
                z: Mathf.Sin(_timer) * _distanceWithCenter
            );
        }
    }
}
