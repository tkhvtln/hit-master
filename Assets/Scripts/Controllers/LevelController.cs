using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

public class LevelController : MonoBehaviour
{
    public WaypointCreator Waypoints => _waypointCreator;
    public NavMeshSurface Surface => _navMeshSurface;

    private WaypointCreator _waypointCreator;
    private NavMeshSurface _navMeshSurface;

    [Inject]
    private void Construct(WaypointCreator waypointCreator, NavMeshSurface navMeshSurface)
    {
        _waypointCreator = waypointCreator;
        _navMeshSurface = navMeshSurface;
    }
}
