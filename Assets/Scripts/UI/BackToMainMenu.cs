
using UnityEngine;
using UnityEngine.UI;

public class BackToMainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private Button backToMainMenu;

    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.onlevelComplete += OnLevelComplete;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.onlevelComplete -= OnLevelComplete;
    }

    private void Start()
    {
        backToMainMenu = GetComponent<Button>();

        backToMainMenu.onClick.AddListener(OnBackToMainMenuClicked);

        Hide();
    }

    private void OnDestroy()
    {
        if (backToMainMenu != null)
            backToMainMenu.onClick.RemoveListener(OnBackToMainMenuClicked);
    }

    private void OnLevelComplete(object sender, System.EventArgs e)
    {
        Show();
    }

    private void OnBackToMainMenuClicked()
    {
        GameManager.Instance.LoadMainMenuScene();
    }

    private void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void Hide()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}