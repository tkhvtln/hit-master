using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(SpawnerEnemy))]
public class EnemySpawnerEditor : Editor
{
    SerializedProperty enemiesProperty;

    private void OnEnable()
    {
        enemiesProperty = serializedObject.FindProperty("_enemyTypeList");
    }

    public override void OnInspectorGUI()
    {
        SpawnerEnemy spawner = (SpawnerEnemy)target;

        serializedObject.Update();

        GUILayout.Label("Enemy classes");
        for (int i = 0; i < enemiesProperty.arraySize; i++)
        {
            SerializedProperty enemyTypeProperty = enemiesProperty.GetArrayElementAtIndex(i);
            SerializedProperty enemyPrefabProperty = enemyTypeProperty.FindPropertyRelative("prefab");
            SerializedProperty isSelectedProperty = enemyTypeProperty.FindPropertyRelative("isSelected");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(enemyPrefabProperty, GUIContent.none);

            bool newIsSelected = EditorGUILayout.Toggle("Select", isSelectedProperty.boolValue);
            if (newIsSelected != isSelectedProperty.boolValue)
            {
                isSelectedProperty.boolValue = newIsSelected;
                if (newIsSelected)
                    DeactiveOtherEnemies(i);
            }

            EditorGUILayout.EndHorizontal();
        }

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("+", GUILayout.Width(30)))
        {
            enemiesProperty.InsertArrayElementAtIndex(enemiesProperty.arraySize);

            SerializedProperty enemyTypeProperty = enemiesProperty.GetArrayElementAtIndex(enemiesProperty.arraySize - 1);
            enemyTypeProperty.FindPropertyRelative("prefab").objectReferenceValue = null;
            enemyTypeProperty.FindPropertyRelative("isSelected").boolValue = false;
        }
        if (GUILayout.Button("-", GUILayout.Width(30)))
        {
            if (enemiesProperty.arraySize > 0)
                enemiesProperty.DeleteArrayElementAtIndex(enemiesProperty.arraySize - 1);
        }
        GUILayout.EndHorizontal();

        if (enemiesProperty.arraySize == 0)
        {
            EditorGUILayout.HelpBox("The enemy list is empty. Please add at least one enemy.", MessageType.Warning);
        }
        if (!IsEnemyActivated())
        {
            EditorGUILayout.HelpBox("Enemy prefab is not assigned or no enemy checkbox is checked. Please assign an enemy prefab and check the checkbox of the enemy you want to spawn.", MessageType.Warning);
        }

        GUILayout.Space(20);

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

        serializedObject.ApplyModifiedProperties();

        base.OnInspectorGUI();
    }

    private void DeactiveOtherEnemies(int activeIndex)
    {
        for (int i = 0; i < enemiesProperty.arraySize; i++)
        {
            if (i != activeIndex)
            {
                SerializedProperty enemyTypeProperty = enemiesProperty.GetArrayElementAtIndex(i);
                SerializedProperty isSelectedProperty = enemyTypeProperty.FindPropertyRelative("isSelected");
                isSelectedProperty.boolValue = false;
            }
        }
    }

    private bool IsEnemyActivated()
    {
        bool isAssigned = false;
        
        for (int i = 0; i < enemiesProperty.arraySize; i++)
        {
            SerializedProperty enemyTypeProperty = enemiesProperty.GetArrayElementAtIndex(i);
            SerializedProperty enemyPrefabProperty = enemyTypeProperty.FindPropertyRelative("prefab");
            SerializedProperty isSelectedProperty = enemyTypeProperty.FindPropertyRelative("isSelected");

            if (enemyPrefabProperty.objectReferenceValue != null && isSelectedProperty.boolValue == true)
                isAssigned = true;
        }

        return isAssigned;
    }
}