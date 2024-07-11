using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{
    protected ReactiveProperty<bool> _isDied = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> IsDied => _isDied;

    public abstract void Init();
    public abstract void Die();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            Die();
        }
    }
}
