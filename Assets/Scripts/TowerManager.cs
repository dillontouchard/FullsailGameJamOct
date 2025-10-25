using UnityEngine;
using UnityEngine.InputSystem;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    public GameObject[] towers;
    public GameObject[] towerOutlines;
    public bool isPickingTower = false;
    public int towerIndex = 0;
    [SerializeField] Camera mainCamera;
    // Private variables for placing towers in the scene
    private bool isPlacingTower = false;
    private GameObject towerToPlace;
    void Awake()
    {
        if(Instance != this)
        {
            Instance = this;
        }
    }

    void Update()
    {
        if(isPickingTower)
        {
            towerToPlace = PickTowerOutline(towerIndex);
        }
        if (isPlacingTower)
        {
            FollowMouse();
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                PlaceTower();
            }
        }
    }
    public GameObject PickTowerOutline(int towerIndex)
    {
        GameObject placing = Instantiate(towerOutlines[towerIndex]);
        isPickingTower = false;
        isPlacingTower = true;
        return placing;
    }

    public void FollowMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            towerToPlace.transform.position = hitInfo.point;
        }
    }

    public void PlaceTower()
    {
        isPlacingTower = false;
        Instantiate(towers[towerIndex], towerToPlace.transform.position, Quaternion.identity);
        Destroy(towerToPlace);
    }
}
