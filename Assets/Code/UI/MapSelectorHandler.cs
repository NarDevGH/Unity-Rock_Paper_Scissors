using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MapSelectorHandler : MonoBehaviour
{
    [SerializeField] private MainMenuPanelsHandler panelHandler;
    private UIDocument uiDocument;

    private Label mapName;
    private Button mapButton;

    private int currentMapIndex = 0;
    private MapData[] maps;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
        GetMaps();
        //SetVisualElements();
        //DisplayCurrentMap();
    }

    private void OnEnable()
    {
        SetVisualElements();
        DisplayCurrentMap();
    }

    private void SetVisualElements()
    {
        mapName = uiDocument.rootVisualElement.Q<Label>("MapName_Label");

        mapButton = uiDocument.rootVisualElement.Q<Button>("SelectMap_Button");
        mapButton.clicked += OnClickSelectMap;

        uiDocument.rootVisualElement.Q<Button>("NextMap_Button").clicked += OnClickNextMap;
        uiDocument.rootVisualElement.Q<Button>("PrevMap_Button").clicked += OnClickPrevtMap;
        uiDocument.rootVisualElement.Q<Button>("Return_Button").clicked += OnClickedReturn;
    }

    private void OnClickNextMap()
    {
        if (++currentMapIndex == maps.Count())
        {
            currentMapIndex = 0;
        }

        DisplayCurrentMap();
    }
    private void OnClickPrevtMap()
    {
        if (--currentMapIndex < 0)
        {
            currentMapIndex = maps.Count()-1;
        }

        DisplayCurrentMap();
    }
    private void OnClickSelectMap()
    {

    }
    private void OnClickedReturn()
    {
        panelHandler.SwitchToMainMenu();
    }

    private void GetMaps()
    {
        maps = Resources.LoadAll<MapData>("Data/MapData");
    }

    private void DisplayCurrentMap()
    {
        mapName.text = maps[currentMapIndex].mapName;

        Background background = new Background();
        background.sprite = maps[currentMapIndex].snapshot;
        mapButton.style.backgroundImage = background;
    }
}
