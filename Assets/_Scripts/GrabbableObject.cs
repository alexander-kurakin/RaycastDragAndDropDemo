using UnityEngine;

public class GrabbableObject : MonoBehaviour, IGrabbable
{
    private bool _isGrabbed = false;
    private Transform _grabPoint;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Grab()
    {
        _isGrabbed = true;
    }

    public void Drop()
    {
        _isGrabbed = false;
    }

    public Rigidbody Rigidbody() => _rigidbody;

    public Transform Transform() => transform;
}
