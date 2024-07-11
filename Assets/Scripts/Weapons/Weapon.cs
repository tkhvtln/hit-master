using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected ProjectileConfig _projectileConfig;

    protected ObjectPool<Projectile> _objectPool;

    public abstract void Init();
    public abstract void Shot(Vector3 targetPosition);
}
