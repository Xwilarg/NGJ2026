using NGJ2026.Manager;
using UnityEngine;

namespace NGJ2026.Insect
{
    public class LookAtCamera : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(InsectManager.Instance.PlayerCameraPos.transform.position);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + 90f, transform.rotation.eulerAngles.y + 180f, transform.rotation.eulerAngles.z);
        }
    }
}
