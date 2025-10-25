using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonFunctions : MonoBehaviour
{
    [SerializeField] GameObject defensesButton;
    public void OpenDefenses()
    {
        defensesButton.SetActive(false);
    }
}
