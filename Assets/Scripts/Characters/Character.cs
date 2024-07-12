using UniRx;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] 
    protected Animator _animator;
    protected Rigidbody[] _rbRagdolls;

    protected ReactiveProperty<bool> _isDied = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> IsDied => _isDied;

    public virtual void Init()
    {
        _rbRagdolls = GetComponentsInChildren<Rigidbody>();
        ActivateRagdoll(false);
    }

    public virtual void Die()
    {
        _isDied.Value = true;
        ActivateRagdoll(true);
    }

    public virtual void ActivateRagdoll(bool activaate)
    {
        _animator.enabled = !activaate;

        foreach (Rigidbody rb in _rbRagdolls)
            rb.isKinematic = !activaate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            Die();
        }
    }
}
