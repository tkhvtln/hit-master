using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterSpawner))]
public class CharacterSpawnerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        CharacterSpawner spawner = (CharacterSpawner)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add enemy"))
        {
            spawner.CreateCharacter(CharacterName.ENEMY);
        }
        if (GUILayout.Button("Add citizen"))
        {
            spawner.CreateCharacter(CharacterName.CITIZEN);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Remove enemy"))
        {
            spawner.RemoveCharacter(CharacterName.ENEMY);
        }
        if (GUILayout.Button("Remove citizen"))
        {
            spawner.RemoveCharacter(CharacterName.CITIZEN);
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Remove all"))
        {
            spawner.RemoveAllCharacters();
        }

        base.OnInspectorGUI();
    }
}