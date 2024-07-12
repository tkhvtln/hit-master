using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyConfig _enemyConfig;

    [Space]
    [SerializeField] protected Animator _animator;
    [SerializeField] private Slider _sliderHealth;

    protected Rigidbody[] _rbRagdolls;
    protected BoxCollider _collider;

    protected ReactiveProperty<int> _health;
    protected ReactiveProperty<bool> _isDied = new ReactiveProperty<bool>();

    public IReadOnlyReactiveProperty<bool> IsDied => _isDied;

    public virtual void Init()
    {
        _health = new ReactiveProperty<int>(_enemyConfig.Health);

        _collider = GetComponent<BoxCollider>();
        _rbRagdolls = GetComponentsInChildren<Rigidbody>();

        ActivateRagdoll(false);

        _sliderHealth.gameObject.SetActive(false);
        this.OnTriggerEnterAsObservable()
            .Where(other => other.TryGetComponent(out Projectile projectile))
            .Take(1)
            .Subscribe(_ => _sliderHealth.gameObject.SetActive(true))
            .AddTo(this);

        _health.Subscribe(x => _sliderHealth.value = (float)x * 100f / (float)_enemyConfig.Health / 100f)
            .AddTo(this);
    }

    public virtual void Die()
    {
        _isDied.Value = true;
        _collider.enabled = false;
        _sliderHealth.gameObject.SetActive(false);
        
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
