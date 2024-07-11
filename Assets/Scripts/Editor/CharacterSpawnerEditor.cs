using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
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
            Undo.RecordObject(spawner, "Add enemy");

            spawner.CreateCharacter(CharacterName.ENEMY);

            EditorUtility.SetDirty(spawner);
            EditorSceneManager.MarkSceneDirty(spawner.gameObject.scene);
        }
        if (GUILayout.Button("Add citizen"))
        {
            Undo.RecordObject(spawner, "Add citizen");

            spawner.CreateCharacter(CharacterName.CITIZEN);

            EditorUtility.SetDirty(spawner);
            EditorSceneManager.MarkSceneDirty(spawner.gameObject.scene);
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Remove enemy"))
        {
            Undo.RecordObject(spawner, "Remove enemy");

            spawner.RemoveCharacter(CharacterName.ENEMY);

            EditorUtility.SetDirty(spawner);
            EditorSceneManager.MarkSceneDirty(spawner.gameObject.scene);
        }
        if (GUILayout.Button("Remove citizen"))
        {
            Undo.RecordObject(spawner, "Remove citizen");

            spawner.RemoveCharacter(CharacterName.CITIZEN);

            EditorUtility.SetDirty(spawner);
            EditorSceneManager.MarkSceneDirty(spawner.gameObject.scene);
        }
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Remove all"))
        {
            Undo.RecordObject(spawner, "Remove all");

            spawner.RemoveAllCharacters();

            EditorUtility.SetDirty(spawner);
            EditorSceneManager.MarkSceneDirty(spawner.gameObject.scene);
        }

        base.OnInspectorGUI();
    }
}