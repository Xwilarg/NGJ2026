using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    private Vector3 _originPoint;
    private Quaternion _originRot;
    private FollowState _followState;

    private float _timer;

    private void Awake()
    {
        _originPoint = transform.position;
        _originRot = transform.rotation;
    }

    public void StartFollowingPlayer()
    {
        _followState = FollowState.FollowDestination;
        _timer = 0f;
    }

    public void StartFollowingOrigin()
    {
        _followState = FollowState.FollowOrigin;
        _timer = 0f;
    }

    public Transform target;
    void Update() {

        if (_followState != FollowState.Disabled)
        {
            if (_timer < 1f)
            {
                _timer += Time.deltaTime;
                if (_timer >= 1f) _timer = 1f;
            }

            var rot = target.eulerAngles;
            rot.z = 0;

            if (_followState == FollowState.FollowDestination)
            {
                transform.position = Vector3.Slerp(_originPoint, target.position, _timer);
                transform.rotation = Quaternion.Lerp(_originRot, Quaternion.Euler(rot), _timer);
            }
            else
            {
                transform.position = Vector3.Slerp(target.position, _originPoint, _timer);
                transform.rotation = Quaternion.Lerp(Quaternion.Euler(rot), _originRot, _timer);
            }
        }
    }

    public enum FollowState
    {
        Disabled,
        FollowDestination,
        FollowOrigin
    }
}
