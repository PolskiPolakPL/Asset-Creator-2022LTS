using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player; 

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
