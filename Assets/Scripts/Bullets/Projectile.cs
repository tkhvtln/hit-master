using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int Damage => _damage;

    protected int _damage;
    protected Transform _transform;

    public abstract void Init(int damage, float speed, float lifetime);
    public abstract void Move(Vector3 targetPosition);
}
