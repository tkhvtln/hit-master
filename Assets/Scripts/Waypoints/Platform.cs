using System;
using System.Linq;
using UniRx;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public event Action OnAllEnemiesDied;

    [SerializeField] private SpawnerEnemy _spawnerEnemy;

    public void Start()
    {
        foreach (Enemy character in _spawnerEnemy.Characters)
        {
            character.Init();

            character.IsDied
                .Where(isDeid => isDeid)
                .Subscribe(_ => CheckAllEnemiesDestroyed());
        }
    }

    private void CheckAllEnemiesDestroyed()
    {
        if (_spawnerEnemy.Characters.All(character => character.IsDied.Value))
        {
            OnAllEnemiesDied?.Invoke();
        }
    }
}
