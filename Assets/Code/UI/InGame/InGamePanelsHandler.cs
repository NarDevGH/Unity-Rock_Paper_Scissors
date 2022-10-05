using UnityEngine;

public class InGamePanelsHandler : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _settingsPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            SwitchToPauseMenuPanel();
        }
    }

    public void SwitchToPauseMenuPanel()
    {
        _pauseMenuPanel.SetActive(true);
        _settingsPanel.SetActive(false);
    }
    public void SwitchToSettingsPanel()
    {
        _pauseMenuPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }
}
