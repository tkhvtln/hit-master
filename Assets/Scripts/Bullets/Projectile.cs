using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected Transform _transform;

    public abstract void Init(float speed, float lifetime);
    public abstract void Move(Vector3 targetPosition);
}
