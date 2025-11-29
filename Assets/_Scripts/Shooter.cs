using UnityEngine;

public class Shooter : IShooter
{
    private IShootEffect _shootEffect;

    public Shooter(IShootEffect shootEffect)
    {
        _shootEffect = shootEffect;
    }

    public void Shoot(Vector3 shootPosition)
    {
        _shootEffect.ProcessEffect(shootPosition);
    }
}