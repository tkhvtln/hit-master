using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Configs/Projectile")]
public class ProjectileConfig : ScriptableObject
{
    public float Speed => _speed;
    public float Lifetime => _lifetime;
    public Projectile Prefab => _prefab;

    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _lifetime = 5f;

    [Space]
    [SerializeField] private Projectile _prefab;
}
