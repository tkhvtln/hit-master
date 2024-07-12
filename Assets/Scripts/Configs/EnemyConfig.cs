using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Configs/Enemy")]
public class EnemyConfig : ScriptableObject
{
    public int Health => _health;

    [SerializeField] private int _health = 100;
}
