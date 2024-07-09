using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] 
    private List<CharacterType> _characterTypeList = new List<CharacterType>();
    private List<Character> _characterCreatedList = new List<Character>();

    private const int OFFSET_RADIUS = 5;

    public void CreateCharacter(CharacterName name)
    {
        Character characterPrefab = _characterTypeList.Find(type => type.name == name).prefab;

        if (characterPrefab != null)
        {
            Character character = Instantiate(characterPrefab, transform);
            character.transform.localScale = GetIgnoreParentScale();

            _characterCreatedList.Add(character);

            SetCharacterPosition();
        }
    }

    public void RemoveCharacter(CharacterName name)
    {
        Character characterPrefab = _characterTypeList.Find(type => type.name == name).prefab;

        if (characterPrefab != null)
        {
            Character character = _characterCreatedList[_characterCreatedList.Count - 1];
            
            _characterCreatedList.Remove(character);
            DestroyImmediate(character.gameObject);

            SetCharacterPosition();
        }
    }

    public void RemoveAllCharacters()
    {
        foreach (Character character in _characterCreatedList)
            DestroyImmediate(character.gameObject);

        _characterCreatedList.Clear();

    }

    private void SetCharacterPosition()
    {
        float radius = transform.localScale.x / 2 - OFFSET_RADIUS;

        for (int i = 0; i < _characterCreatedList.Count; i++)
        {
            float angle = (i * Mathf.PI / _characterCreatedList.Count) + (Mathf.PI / 20); 
            Vector3 spawnPosition = new Vector3(Mathf.Cos(angle) * radius, transform.localScale.y, Mathf.Sin(angle) * radius) + transform.position;
            _characterCreatedList[i].transform.position = spawnPosition;
        }
    }

    private Vector3 GetIgnoreParentScale()
    {
        Vector3 parentScale = transform.localScale;
        Vector3 inverseScale = new Vector3(1f / parentScale.x, 1f / parentScale.y, 1f / parentScale.z);
        return inverseScale;
    }
}




