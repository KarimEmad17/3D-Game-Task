using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsNewGame { get; private set; }

    private const string GameScene = "Game";
    private const string LoaderScene = "LoadingScene";
    private const string MainMenuScene = "MainMenu";
    public event EventHandler onlevelComplete;
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void StartNewGame()
    {
        IsNewGame = true;

        LoadGameScene();
    }

    public void ContinueGame()
    {
        IsNewGame = false;

        LoadGameScene();
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(LoaderScene);
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(MainMenuScene);
    }
    public void CompleteLevel()
    {
        Debug.Log("Level Completed!");

        // You can show a level-complete UI here.

        onlevelComplete?.Invoke(this, EventArgs.Empty);
    }
    public bool HasSave()
    {
        return SaveManager.HasSaveFile();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}