using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameController _gameController;

    [Space]
    [SerializeField] private SaveController _saveController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private SoundController _soundController;
    
    public override void InstallBindings()
    {
        Container.Bind<GameController>().FromInstance(_gameController).AsSingle().NonLazy();
        Container.Bind<SaveController>().FromInstance(_saveController).AsSingle().NonLazy();
        Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle().NonLazy();
        Container.Bind<SoundController>().FromInstance(_soundController).AsSingle().NonLazy();
    }
}