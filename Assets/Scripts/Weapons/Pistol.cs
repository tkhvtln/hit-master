using UnityEngine;

public class Pistol : Weapon
{
    public override void Init()
    {
        _objectPool = new ObjectPool<Projectile>(_projectileConfig.Prefab, 20, transform, true);
        foreach (Projectile projectile in _objectPool.pool)
            projectile.Init(_projectileConfig.Speed, _projectileConfig.Lifetime);
    }

    public override void Shot(Vector3 targetPosition)
    {
        _objectPool.GetFreeElement().Move(targetPosition);
    }
}
