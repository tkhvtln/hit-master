using UniRx;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    protected ReactiveProperty<int> _health = new ReactiveProperty<int>(100);

    [SerializeField] protected Animator _animator;
    [SerializeField] private Slider _sliderHealth;

    protected Rigidbody[] _rbRagdolls;

    protected ReactiveProperty<bool> _isDied = new ReactiveProperty<bool>();
    public IReadOnlyReactiveProperty<bool> IsDied => _isDied;

    public virtual void Init()
    {
        _rbRagdolls = GetComponentsInChildren<Rigidbody>();
        ActivateRagdoll(false);

        _health.Subscribe(x => _sliderHealth.value = (float)x / 100f)
            .AddTo(this);
    }

    public virtual void Die()
    {
        _sliderHealth.gameObject.SetActive(false);

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
            _health.Value -= projectile.Damage;

            if (_health.Value <= 0)
                Die();
        }
    }
}
