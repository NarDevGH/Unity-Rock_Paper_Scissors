using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private MainMenuPanelsHandler panelHandler;
    private UIDocument uiDocument;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        uiDocument.rootVisualElement.Q<Button>("Play_Button").clicked += OnClickedPlay;
        uiDocument.rootVisualElement.Q<Button>("Settings_Button").clicked += OnClickedSettings;
    }

    private void OnClickedPlay()
    {
        panelHandler.SwitchToMapSelectorMenu();
    }
    private void OnClickedSettings()
    {
        panelHandler.SwitchToSettingMenu();
    }
}
