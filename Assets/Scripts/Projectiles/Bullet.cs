using UnityEngine;
using UniRx;
using System;

public class Bullet : Projectile
{
    public override void Init(int damage, float speed, float lifetime)
    {
        _damage = damage;
        _speed = speed;
        _lifetime = lifetime;

        _transform = transform;
    }

    public override void Move(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - _transform.position).normalized;
        direction.y = 0;
        _transform.forward = direction;

        Observable.EveryUpdate()
            .TakeUntil(Observable.Timer(TimeSpan.FromSeconds(_lifetime)))
            .TakeUntilDisable(this)
            .Subscribe(_ =>
            {
                _transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            },
            () =>
            {
                gameObject.SetActive(false);
                _transform.localPosition = Vector3.zero;
            })
            .AddTo(this);
    }
}
