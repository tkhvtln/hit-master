using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public event Action OnAllEnemiesDied;

    [SerializeField] private CharacterSpawner _characterSpawner;

    public void Start()
    {
        foreach (Character character in _characterSpawner.Characters)
            character.IsDied
                .Where(isDeid => isDeid)
                .Subscribe(_ => CheckAllEnemiesDestroyed());
    }

    private void CheckAllEnemiesDestroyed()
    {
        if (_characterSpawner.Characters.All(character => character.IsDied.Value))
        {
            OnAllEnemiesDied?.Invoke();
        }
    }
}
