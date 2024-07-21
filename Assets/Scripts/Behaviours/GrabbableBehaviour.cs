using UnityEngine;

public class GrabbableBehaviour : ITakeable
{
    protected Transform _grabPoint;
    protected Rigidbody _rigidBody;
    protected Transform _transform;

    public GrabbableBehaviour(Rigidbody rb, Transform transform)
    {
        _rigidBody = rb;
        _transform = transform;
        _grabPoint = PlayerInteractions.Instance.GetGrabPoint();
    }

    public void Take()
    {
        _rigidBody.isKinematic = false;
        _rigidBody.useGravity = false;

        UpdateCaller.OnFixedUpdate += ChangePosition;
    }
    
    public void ChangePosition()
    {
        _rigidBody.MovePosition(_grabPoint.position);
    }

    public void Drop()
    {
        UpdateCaller.OnFixedUpdate -= ChangePosition;
        _transform.parent = null;
        _rigidBody.useGravity = true;
    }
}
