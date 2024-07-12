using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private LevelController _levelController;
    [SerializeField] private WaypointCreator _waypointCreator;
    [SerializeField] private NavMeshSurface _navMeshSurface; 
    
    public override void InstallBindings()
    {
        Container.Bind<LevelController>().FromInstance(_levelController).AsSingle().NonLazy();
        Container.Bind<WaypointCreator>().FromInstance(_waypointCreator).AsSingle().NonLazy();
        Container.Bind<NavMeshSurface>().FromInstance(_navMeshSurface).AsSingle().NonLazy();
    }
}