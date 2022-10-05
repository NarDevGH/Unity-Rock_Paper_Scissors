using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainSettingsMenuHandler : MonoBehaviour
{
    [SerializeField] private MainMenuPanelsHandler panelHandler;
    private UIDocument uiDocument;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        uiDocument.rootVisualElement.Q<Button>("Return_Button").clicked += OnClickedReturn;
    }

    private void OnClickedReturn()
    {
        panelHandler.SwitchToMainMenu();
    }
}
