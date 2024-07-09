using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private UIController _uiController;

    [Space]
    [SerializeField] private PanelMenu _panelMenu;
    [SerializeField] private PanelGame _panelGame;
    [SerializeField] private PanelWin _panelWin;
    [SerializeField] private PanelDefeat _panelDefeat;

    public override void InstallBindings()
    {
        Container.Bind<UIController>().FromInstance(_uiController).AsSingle().NonLazy();

        Container.Bind<PanelMenu>().FromInstance(_panelMenu).AsSingle().NonLazy();
        Container.Bind<PanelGame>().FromInstance(_panelGame).AsSingle().NonLazy();
        Container.Bind<PanelWin>().FromInstance(_panelWin).AsSingle().NonLazy();
        Container.Bind<PanelDefeat>().FromInstance(_panelDefeat).AsSingle().NonLazy();
    }
}