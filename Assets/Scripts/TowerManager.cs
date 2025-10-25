using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance { get; private set; }

    public GameObject[] towers;
    public bool isPickingTower = false;
    public int towerIndex = 0;
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
            towerToPlace = PickTower(towerIndex);
        }
        if (isPlacingTower)
        {
            towerToPlace.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        }
    }
    public GameObject PickTower(int towerIndex)
    {
        GameObject placing = Instantiate(towers[towerIndex]);
        isPickingTower = false;
        isPlacingTower = true;
        return placing;
    }
}
