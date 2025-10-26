using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtonFunctions : MonoBehaviour
{
    // Game UI Buttons
    [SerializeField] GameObject defensesButton;
    [SerializeField] GameObject guardHouseButton;
    [SerializeField] GameObject throwerButton;

    // Main Menu Panels
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject mainMenuPanel;
    public void OpenDefenses()
    {
        defensesButton.SetActive(false);
        guardHouseButton.SetActive(true);
        throwerButton.SetActive(true);
    }

    public void SelectGuardHouse()
    {
        throwerButton.SetActive(false);
        guardHouseButton.SetActive(false);
        TowerManager.Instance.towerIndex = 0;
        TowerManager.Instance.isPickingTower = true;
    }

    public void SelectThrower()
    {
        guardHouseButton.SetActive(false);
        throwerButton.SetActive(false);
        TowerManager.Instance.towerIndex = 1;
        TowerManager.Instance.isPickingTower = true;
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Dev");
    }

    public void OpenCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
