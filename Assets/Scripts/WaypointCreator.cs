using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

[ExecuteInEditMode]
public class WaypointCreator : MonoBehaviour
{
    #region GETTERS
    public List<Platform> PlatformList => _platformList;
    #endregion

    #region SERIALIZE FIELD
    [SerializeField] private GameObject _bridgePrefab;
    [SerializeField] private Platform _platformPrefab;

    [Space]
    [SerializeField] private bool _autoBake = true;
    [SerializeField] private int defaultDistanceBetweenPlatforms = 25;
    [SerializeField] private NavMeshSurface _navMeshSurface;

    [SerializeField] private List<Platform> _platformList = new List<Platform>();
    [SerializeField] private List<GameObject> _bridgeList = new List<GameObject>();
    [SerializeField] private List<Vector3> _previousPositionsList = new List<Vector3>();
    #endregion

    #region PLATFORM
    public void CreatePlatform()
    {
        Vector3 position = new Vector3(0, 0, _platformList.Count * defaultDistanceBetweenPlatforms);
        Platform platform = Instantiate(_platformPrefab, position, Quaternion.identity, transform);

        _platformList.Add(platform);
        _previousPositionsList.Add(platform.transform.position);

        if (_platformList.Count > 1)
            CreateBridge(_platformList[_platformList.Count - 2], _platformList[_platformList.Count - 1]);

        BakeNavMesh();
    }

    public void RemovePlatform()
    {
        if (_platformList.Count > 0)
        {
            RemoveBridge();

            Platform paltform = _platformList[_platformList.Count - 1];

            _platformList.RemoveAt(_platformList.Count - 1);
            _previousPositionsList.RemoveAt(_previousPositionsList.Count - 1);

            DestroyImmediate(paltform.gameObject);
            BakeNavMesh();
        }
    }

    public void RemoveAllPlatforms()
    {
        foreach (Platform paltform in _platformList)
            DestroyImmediate(paltform.gameObject);

        foreach (GameObject paltform in _bridgeList)
            DestroyImmediate(paltform.gameObject);

        _bridgeList.Clear();
        _platformList.Clear();
        _previousPositionsList.Clear();

        BakeNavMesh();
    }

    private void UpdatePlatforms()
    {
        for (int i = 1; i < _platformList.Count; i++)
        {
            _platformList[i].transform.LookAt(_platformList[i - 1].transform);
            _platformList[i].transform.Rotate(0, 180, 0);
        }
    }
    #endregion

    #region BRIDGE
    private void CreateBridge(Platform platform1, Platform platform2)
    {
        GameObject bridge = Instantiate(_bridgePrefab, transform);
        _bridgeList.Add(bridge);

        UpdateBridges();
    }

    private void RemoveBridge()
    {
        if (_bridgeList.Count > 0)
        {
            GameObject bridge = _bridgeList[_bridgeList.Count - 1];
            _bridgeList.RemoveAt(_bridgeList.Count - 1);

            DestroyImmediate(bridge);
        }
    }

    private void UpdateBridges()
    {
        for (int i = 0; i < _bridgeList.Count; i++)
        {
            GameObject bridge = _bridgeList[i];

            Vector3 position1 = GetBridgePosition(_platformList[i]);
            Vector3 position2 = GetBridgePosition(_platformList[i + 1]);

            bridge.transform.position = (position1 + position2) / 2;
            bridge.transform.LookAt(position2);

            float distance = Vector3.Distance(position1, position2) - _platformPrefab.transform.localScale.z + 5;
            bridge.transform.localScale = new Vector3(bridge.transform.localScale.x, bridge.transform.localScale.y, distance);
        }

        BakeNavMesh();
    }

    private Vector3 GetBridgePosition(Platform platform)
    {
        float offset = _bridgePrefab.transform.localScale.y * 0.5f;
        float height = platform.transform.localScale.y * 0.5f - offset;

        Vector3 position = platform.transform.position + Vector3.up * height;
        return position;
    }
    #endregion

    #region NavMesh
    private void BakeNavMesh()
    {
        if (_autoBake)
            _navMeshSurface.BuildNavMesh();
    }
    #endregion

    #region UNITY
    private void Update()
    {
        if (!Application.isPlaying)
        {
            bool isPositionChanged = false;

            for (int i = 0; i < _platformList.Count; i++)
            {
                if (_platformList[i].transform.position != _previousPositionsList[i])
                {
                    isPositionChanged = true;
                    _previousPositionsList[i] = _platformList[i].transform.position;
                }
            }

            if (isPositionChanged)
            {
                UpdatePlatforms();
                UpdateBridges();
            }
        }
    }
    #endregion
}
