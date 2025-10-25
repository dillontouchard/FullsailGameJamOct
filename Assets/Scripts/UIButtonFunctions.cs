using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonFunctions : MonoBehaviour
{
    [SerializeField] GameObject defensesButton;
    [SerializeField] GameObject guardHouseButton;
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
}
