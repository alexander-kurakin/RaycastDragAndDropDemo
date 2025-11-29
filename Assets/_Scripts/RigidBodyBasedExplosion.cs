using UnityEngine;

public class RigidBodyBasedExplosion : IShootEffect
{
    private float _explosionRadius;
    private float _explosionForce;

    public RigidBodyBasedExplosion(float explosionRadius, float explosionForce) 
    { 
        _explosionRadius = explosionRadius;
        _explosionForce = explosionForce;
    }
    
    public void ProcessEffect(Vector3 shootPosition)
    {
        Collider[] targets = Physics.OverlapSphere(shootPosition, _explosionRadius);

        foreach (Collider target in targets)
        {
            Rigidbody rigidbody = target.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                Vector3 direction = target.transform.position - shootPosition;
                rigidbody.AddForce(direction.normalized * _explosionForce);
            }
        }
    }
}
