using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaypointCreator))]
public class WaypointCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WaypointCreator creator = (WaypointCreator)target;
      
        if (GUILayout.Button("Add waypoint"))
        {
           
            creator.CreatePlatform();
        }
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Remove waypoint"))
        {
            creator.RemovePlatform();
        }
        if (GUILayout.Button("Remove all waypoints"))
        {
            creator.RemoveAllPlatforms();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        base.OnInspectorGUI();
    }
}
