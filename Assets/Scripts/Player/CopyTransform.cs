using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    private Vector3 _originPoint;
    private Quaternion _originRot;
    private bool _followPlayer;

    private float _timer;

    private void Awake()
    {
        _originPoint = transform.position;
        _originRot = transform.rotation;
    }

    public void StartFollowingPlayer()
    {
        _followPlayer = true;
    }

    public Transform target;
    void Update() {

        if (_followPlayer)
        {
            if (_timer < 1f)
            {
                _timer += Time.deltaTime;
                if (_timer >= 1f) _timer = 1f;
            }

            transform.position = Vector3.Slerp(_originPoint, target.position, _timer);
            var rot = target.eulerAngles;
            rot.z = 0;
            transform.rotation = Quaternion.Lerp(_originRot, Quaternion.Euler(rot), _timer);
        }
    }
}
