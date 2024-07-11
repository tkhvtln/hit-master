using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(WaypointCreator))]
public class WaypointCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WaypointCreator creator = (WaypointCreator)target;
      
        if (GUILayout.Button("Add waypoint"))
        {
            Undo.RecordObject(creator, "Add waypoint");

            creator.CreatePlatform();

            EditorUtility.SetDirty(creator);
            EditorSceneManager.MarkSceneDirty(creator.gameObject.scene);
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Remove waypoint"))
        {
            Undo.RecordObject(creator, "Remove waypoint");

            creator.RemovePlatform();

            EditorUtility.SetDirty(creator);
            EditorSceneManager.MarkSceneDirty(creator.gameObject.scene);
        }
        if (GUILayout.Button("Remove all waypoints"))
        {
            Undo.RecordObject(creator, "Remove all waypoints");

            creator.RemoveAllPlatforms();

            EditorUtility.SetDirty(creator);
            EditorSceneManager.MarkSceneDirty(creator.gameObject.scene);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        base.OnInspectorGUI();
    }
}
