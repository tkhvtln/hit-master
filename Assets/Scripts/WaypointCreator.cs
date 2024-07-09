using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WaypointCreator : MonoBehaviour
{
    #region SERIALIZE FIELD
    [SerializeField] private GameObject bridgePrefab;
    [SerializeField] private GameObject platformPrefab;
    #endregion

    #region FIELD
    private List<GameObject> platformList = new List<GameObject>();
    private List<GameObject> bridgeList = new List<GameObject>();
    private List<Vector3> previousPositionsList = new List<Vector3>();
    #endregion

    #region PLATFORM
    public void CreatePlatform()
    {
        Vector3 position = new Vector3(0, 0, platformList.Count * 2);
        GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity, transform);

        platformList.Add(platform);
        previousPositionsList.Add(platform.transform.position);

        if (platformList.Count > 1)
            CreateBridge(platformList[platformList.Count - 2], platformList[platformList.Count - 1]);
    }

    public void RemovePlatform()
    {
        if (platformList.Count > 0)
        {
            RemoveBridge();

            GameObject item = platformList[platformList.Count - 1];

            platformList.RemoveAt(platformList.Count - 1);
            previousPositionsList.RemoveAt(previousPositionsList.Count - 1);

            DestroyImmediate(item);
        }
    }

    public void RemoveAllPlatforms()
    {
        foreach (GameObject item in platformList)
            DestroyImmediate(item);

        foreach (GameObject item in bridgeList)
            DestroyImmediate(item);

        bridgeList.Clear();
        platformList.Clear();
        previousPositionsList.Clear();
    }
    #endregion

    #region BRIDGE
    private void CreateBridge(GameObject platform1, GameObject platform2)
    {
        GameObject bridge = Instantiate(bridgePrefab, transform);
        bridgeList.Add(bridge);

        UpdateBridges();
    }

    private void RemoveBridge()
    {
        if (bridgeList.Count > 0)
        {
            GameObject item = bridgeList[bridgeList.Count - 1];
            bridgeList.RemoveAt(bridgeList.Count - 1);

            DestroyImmediate(item);
        }
    }

    private void UpdateBridges()
    {
        for (int i = 0; i < bridgeList.Count; i++)
        {
            GameObject bridge = bridgeList[i];

            Vector3 position1 = GetBridgePosition(platformList[i]);
            Vector3 position2 = GetBridgePosition(platformList[i + 1]);

            bridge.transform.position = (position1 + position2) / 2;
            bridge.transform.LookAt(position2);

            float distance = Vector3.Distance(position1, position2);
            bridge.transform.localScale = new Vector3(bridge.transform.localScale.x, bridge.transform.localScale.y, distance);
        }
    }

    private Vector3 GetBridgePosition(GameObject platform)
    {
        float offset = bridgePrefab.transform.localScale.y * 1.25f;
        float height = platform.transform.localScale.y * 2 - offset;

        Vector3 position = platform.transform.position + Vector3.up * height / 2;
        return position;
    }
    #endregion

    #region UNITY
    private void Update()
    {
        if (!Application.isPlaying)
        {
            bool isPositionChanged = false;

            for (int i = 0; i < platformList.Count; i++)
            {
                if (platformList[i].transform.position != previousPositionsList[i])
                {
                    isPositionChanged = true;
                    previousPositionsList[i] = platformList[i].transform.position;
                }
            }

            if (isPositionChanged)
                UpdateBridges();
        }
    }
    #endregion
}
