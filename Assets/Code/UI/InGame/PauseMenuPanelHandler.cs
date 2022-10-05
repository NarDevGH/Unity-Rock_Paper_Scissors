using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenuPanelHandler : MonoBehaviour
{
    [SerializeField] private InGamePanelsHandler panelsHandler;
    private UIDocument _uiDocument;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _uiDocument.rootVisualElement.Q<Button>("Continue_Button").clicked += OnClickContinue;
        _uiDocument.rootVisualElement.Q<Button>("Settings_Button").clicked += () => panelsHandler.SwitchToSettingsPanel();
        _uiDocument.rootVisualElement.Q<Button>("MainMenu_Button").clicked += OnClickMainMenu;
    }

    private void OnClickContinue()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    private void OnClickMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
