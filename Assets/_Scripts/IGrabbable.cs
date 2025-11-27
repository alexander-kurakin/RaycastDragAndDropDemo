using UnityEngine;

public interface IGrabbable
{
    public void Grab();
    public void Drop();

    public Rigidbody Rigidbody();
    public Transform Transform();
}
