using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Configs/Player")]
public class PlayerConfig : ScriptableObject
{
    public float SpeedMove => _speedMove;
    public float SpeedRotate => _speedRotate;
    public Weapon CurrentWeapon => _weapon;

    [SerializeField] private float _speedMove = 5;
    [SerializeField] private float _speedRotate = 10;

    [Space]
    [SerializeField] private Weapon _weapon;
}
