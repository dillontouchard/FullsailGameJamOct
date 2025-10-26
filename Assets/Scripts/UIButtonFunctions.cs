using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIButtonFunctions : MonoBehaviour
{
    [SerializeField] GameObject defensesButton;
    [SerializeField] GameObject guardHouseButton;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject mainMenuPanel;
    public void OpenDefenses()
    {
        defensesButton.SetActive(false);
        guardHouseButton.SetActive(true);
    }

    public void SelectGuardHouse()
    {
        guardHouseButton.SetActive(false);
        TowerManager.Instance.towerIndex = 0;
        TowerManager.Instance.isPickingTower = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Dev");
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
