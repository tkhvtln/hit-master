using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaypointCreator))]
public class WaypointCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WaypointCreator waypointCreator = (WaypointCreator)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create waypoint"))
        {
            waypointCreator.CreatePlatform();
        }
        if (GUILayout.Button("Remove waypoint"))
        {
            waypointCreator.RemovePlatform();
        }
        if (GUILayout.Button("Remove all waypoints"))
        {
            waypointCreator.RemoveAllPlatforms();
        }
        GUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }
}
