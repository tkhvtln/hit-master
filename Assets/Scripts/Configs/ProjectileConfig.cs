using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Configs/Projectile")]
public class ProjectileConfig : ScriptableObject
{
    public int Damage => _damage;
    public float Speed => _speed;
    public float Lifetime => _lifetime;
    public Projectile Prefab => _prefab;

    [SerializeField] private int _damage = 50;
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _lifetime = 5f;

    [Space]
    [SerializeField] private Projectile _prefab;
}
