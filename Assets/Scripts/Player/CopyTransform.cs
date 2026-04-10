using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform target;
    void Update() {
        transform.position = target.position;
        var rot = target.eulerAngles;
        rot.z = 0;
        transform.rotation = Quaternion.Euler(rot);
    }
}
