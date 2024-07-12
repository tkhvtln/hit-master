using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(SpawnerEnemy))]
public class EnemySpawnerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        SpawnerEnemy spawner = (SpawnerEnemy)target;

        if (GUILayout.Button("Add enemy"))
        {
            Undo.RecordObject(spawner, "Add enemy");

            spawner.CreateEnemy();

            EditorUtility.SetDirty(spawner);
            EditorSceneManager.MarkSceneDirty(spawner.gameObject.scene);
        }    

        if (GUILayout.Button("Remove enemy"))
        {
            Undo.RecordObject(spawner, "Remove enemy");

            spawner.RemoveEnemy();

            EditorUtility.SetDirty(spawner);
            EditorSceneManager.MarkSceneDirty(spawner.gameObject.scene);
        }

        if (GUILayout.Button("Remove all"))
        {
            Undo.RecordObject(spawner, "Remove all");

            spawner.RemoveAllEnemy();

            EditorUtility.SetDirty(spawner);
            EditorSceneManager.MarkSceneDirty(spawner.gameObject.scene);
        }

        base.OnInspectorGUI();
    }
}