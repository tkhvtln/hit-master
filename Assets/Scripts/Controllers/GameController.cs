using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameController : MonoBehaviour
{
    public bool IsGame { get; private set; }
    private bool _isSceneLoaded;

    private SaveController _saveController;
    private PlayerController _playerController;
    private UIController _uiController;

    [Inject]
    private void Construct(SaveController saveController, PlayerController playerController, UIController uIController)
    {
        _saveController = saveController;
        _playerController = playerController;
        _uiController = uIController;

        saveController.Load();
        LoadCurrentLevel();
    }

    public void Game() 
    {
        IsGame = true;
        _uiController.ShowPanelGame();
    }

    public void Win()
    {
        IsGame = false;
        _uiController.ShowPanelWin();
    }

    public void Defeat() 
    {
        IsGame = false;
        _uiController.ShowPanelDefeat();
    }

    public void LoadCurrentLevel() 
    {
        UnloadScene();
        LoadScene();
    }

    public void LoadNextLevel() 
    {
        UnloadScene();

        _saveController.data.level = ++_saveController.data.level >= SceneManager.sceneCountInBuildSettings ? 1 : _saveController.data.level;
        _saveController.Save();

        LoadScene();
    }

    private void LoadScene()
    {
        if (!_isSceneLoaded)
        {
            _isSceneLoaded = true;
            SceneManager.LoadSceneAsync(_saveController.data.level, LoadSceneMode.Additive);
        }

        _uiController.ShowPanelMenu();
    }

    private void UnloadScene()
    {
        if (_isSceneLoaded)
        {
            _isSceneLoaded = false;
            SceneManager.UnloadSceneAsync(_saveController.data.level);
        }
    }
}
