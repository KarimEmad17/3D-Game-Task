using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    private void Start()
    {
        continueButton.interactable = GameManager.Instance.HasSave();
    }
    public void OnNewGameClicked()
    {
        GameManager.Instance.StartNewGame();
    }

    public void OnContinueClicked()
    {
        GameManager.Instance.ContinueGame();
    }

    public void OnQuitClicked()
    {
        GameManager.Instance.QuitGame();
    }
}