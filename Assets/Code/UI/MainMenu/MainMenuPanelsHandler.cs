using UnityEngine;

public class MainMenuPanelsHandler : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject mapSelectorPanel;
    [SerializeField] private GameObject settingsMenuPanel;
    
    private void Awake()
    {
        SwitchToMainMenu();
    }

    public void SwitchToMainMenu()
    {
        mapSelectorPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void SwitchToSettingMenu()
    {
        mapSelectorPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }

    public void SwitchToMapSelectorMenu()
    {
        mainMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(false);
        mapSelectorPanel.SetActive(true);
    }

}
