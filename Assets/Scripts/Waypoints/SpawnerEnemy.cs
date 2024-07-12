using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public List<Enemy> Characters => _enemyCreatedList;

    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private List<Enemy> _enemyCreatedList = new List<Enemy>();

    private const int OFFSET_RADIUS = 3;

    public void CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemyPrefab, transform);
        enemy.transform.localScale = GetIgnoreParentScale();
        enemy.transform.LookAt(-transform.forward);

        _enemyCreatedList.Add(enemy);

        SetCharacterPosition();
    }

    public void RemoveEnemy()
    {
        if (_enemyCreatedList.Count < 1)
            return;

        Enemy enemy = _enemyCreatedList[_enemyCreatedList.Count - 1];

        _enemyCreatedList.Remove(enemy);
        DestroyImmediate(enemy.gameObject);

        SetCharacterPosition();
    }

    public void RemoveAllEnemy()
    {
        foreach (Enemy enemy in _enemyCreatedList)
            DestroyImmediate(enemy.gameObject);

        _enemyCreatedList.Clear();

    }

    private void SetCharacterPosition()
    {
        float radius = transform.localScale.x / 2 - OFFSET_RADIUS;

        if (_enemyCreatedList.Count == 1)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * transform.localScale.y * 0.5f + transform.forward * transform.localScale.z * 0.25f;
            _enemyCreatedList[0].transform.position = spawnPosition;
        }
        else
        {
            for (int i = 0; i < _enemyCreatedList.Count; i++)
            {
                float angle = (i * Mathf.PI / (_enemyCreatedList.Count - 1)) - (Mathf.PI / 2) + (Mathf.PI / 2);
                Vector3 spawnPosition = new Vector3(Mathf.Cos(angle) * radius, transform.localScale.y * 0.5f, Mathf.Sin(angle) * radius) + transform.position;
                _enemyCreatedList[i].transform.position = spawnPosition;
            }
        }
    }

    private Vector3 GetIgnoreParentScale()
    {
        Vector3 parentScale = transform.localScale;
        Vector3 inverseScale = new Vector3(1f / parentScale.x, 1f / parentScale.y, 1f / parentScale.z);
        return inverseScale;
    }
}




