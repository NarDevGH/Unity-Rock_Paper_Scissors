using UnityEngine;
using UnityEngine.UIElements;

public class InGameSettingsPanelHandler : MonoBehaviour
{
    [SerializeField] private InGamePanelsHandler panelsHandler;
    private UIDocument _uiDocument;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _uiDocument.rootVisualElement.Q<Button>("Return_Button").clicked += () => panelsHandler.SwitchToPauseMenuPanel();
    }
}
