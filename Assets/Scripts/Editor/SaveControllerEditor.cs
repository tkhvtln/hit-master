using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveController))]
public class SaveControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10);

        if (GUILayout.Button("Reset save"))
            PlayerPrefs.DeleteAll();
    }
}