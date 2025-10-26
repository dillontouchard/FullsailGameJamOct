using UnityEngine;
using UnityEngine.InputSystem;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    public GameObject[] towers;
    public GameObject[] towerOutlines;
    public int towerIndex = 0;
    [SerializeField] Camera mainCamera;
    [SerializeField] private float placementCheckArea;
    public bool isPickingTower = false;
    private bool isPlacingTower = false;
    private bool isValidPlacement = false;
    private bool isPlacingThrower = false;
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
        if (isPickingTower)
        {
            towerToPlace = PickTowerOutline(towerIndex);
        }
        if (isPlacingTower && !isPlacingThrower)
        {
            FollowMouse();
            isValidPlacement = IsValidHousePlacement();
            SetPreviewColor(isValidPlacement ? Color.green : Color.red);
            if (Mouse.current.leftButton.wasPressedThisFrame && isValidPlacement)
            {
                PlaceTower();
            }
        }
        else if (isPlacingTower && isPlacingThrower)
        {
            FollowMouse();
            isValidPlacement = IsValidThrowerPlacement();
            SetPreviewColor(isValidPlacement ? Color.green : Color.red);
            if (Mouse.current.leftButton.wasPressedThisFrame && isValidPlacement)
            {
                PlaceTower();
                isPlacingThrower = false;
            }
        }
    }
    public GameObject PickTowerOutline(int towerIndex)
    {
        if(towerIndex == 1)
        {
            isPlacingThrower = true;
        }
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
        isValidPlacement = false;
    }

    public bool IsValidHousePlacement()
    {
        Vector3 location = new( towerToPlace.transform.position.x, towerToPlace.transform.position.y, towerToPlace.transform.position.z + 6);
        Collider[] overlaps = Physics.OverlapSphere(location, placementCheckArea, LayerMask.GetMask("Obstacle"));
        return overlaps.Length == 0;
    }

    public bool IsValidThrowerPlacement()
    {
        Vector3 location = new(towerToPlace.transform.position.x, towerToPlace.transform.position.y, towerToPlace.transform.position.z + 6);
        Collider[] overlaps = Physics.OverlapSphere(location, placementCheckArea, LayerMask.GetMask("Ground"));
        return overlaps.Length == 0;
    }

    public void SetPreviewColor(Color color)
    {
        Renderer[] renderer = towerToPlace.GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderer)
        {
            rend.material.color = color;
        }
    }
}
