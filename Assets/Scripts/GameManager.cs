using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] public int numOfEnemies;
    void Start()
    {
        if(Instance != this)
        {
            Instance = this;
        }
    }

    void Update()
    {
        if(numOfEnemies <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}
