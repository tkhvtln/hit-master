using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Enemy : Character
{
    public override void Init()
    {
        
    }

    public override void Die()
    {
        _isDied.Value = true;
        gameObject.SetActive(false);
    }
}
