using UnityEngine;
using Zenject;

public class UIController : MonoBehaviour
{
    private PanelMenu _panelMenu;
    private PanelGame _panelGame;
    private PanelWin _panelWin;
    private PanelDefeat _panelDefeat;

    private GameController _gameController;

    [Inject]
    private void Construct(PanelMenu panelMenu, PanelGame panelGame, PanelWin panelWin, PanelDefeat panelDefeat, GameController gameController) 
    {
        _panelMenu = panelMenu;
        _panelGame = panelGame;
        _panelWin = panelWin;
        _panelDefeat = panelDefeat;

        _gameController = gameController; 
    }

    public void ShowPanelMenu() 
    {
        Clear();
        _panelMenu.Show();
    }

    public void ShowPanelGame() 
    {
        Clear();
        _panelGame.Show();
    }

    public void ShowPanelWin() 
    {
        Clear();
        _panelWin.Show();
    }

    public void ShowPanelDefeat() 
    {
        Clear();
        _panelDefeat.Show();
    }

    public void OnButtonPlay() 
    {
        _gameController.Game();
    }

    public void OnButtonNextLevel() 
    {
        _gameController.LoadNextLevel();
    }

    public void OnButtonRestartLevel() 
    {
        _gameController.LoadCurrentLevel();
    }

    public void Clear() 
    {
        _panelMenu.Hide();
        _panelGame.Hide();
        _panelWin.Hide();
        _panelDefeat.Hide();
    }
}
