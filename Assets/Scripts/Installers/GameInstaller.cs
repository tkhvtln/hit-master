using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameController _gameController;
    [SerializeField] private SaveController _saveController;
    [SerializeField] private PlayerController _playerController;
    
    public override void InstallBindings()
    {
        Container.Bind<GameController>().FromInstance(_gameController).AsSingle().NonLazy();
        Container.Bind<SaveController>().FromInstance(_saveController).AsSingle().NonLazy();
        Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle().NonLazy();
    }
}