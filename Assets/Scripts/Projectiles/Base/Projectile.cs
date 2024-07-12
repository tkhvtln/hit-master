using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int Damage => _damage;

    protected int _damage;
    protected float _speed;
    protected float _lifetime;

    protected Transform _transform;

    public abstract void Init(int damage, float speed, float lifetime);
    public abstract void Move(Vector3 targetPosition);

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        _transform.localPosition = Vector3.zero;
    }
}
