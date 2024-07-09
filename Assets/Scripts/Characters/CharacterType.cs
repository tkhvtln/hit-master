using System;

[Serializable]
public class CharacterType
{
    public CharacterName name;
    public Character prefab;
}

public enum CharacterName
{
    ENEMY,
    CITIZEN
}

