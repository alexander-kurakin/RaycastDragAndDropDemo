using UnityEngine;

public class EffectsSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;
    public void SpawnExplosionEffect(Vector3 spawnPosition)
    {
        Instantiate(_explosionEffect, spawnPosition, Quaternion.identity);
    }
}
