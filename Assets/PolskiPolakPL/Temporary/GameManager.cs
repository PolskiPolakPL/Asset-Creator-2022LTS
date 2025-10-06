using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public UnityEvent OnPlayerFreeze;
    public UnityEvent OnPlayerUnfreeze;
    bool hasControl;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        hasControl = Player.GetComponent<FPSMovement>().canControl;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ToggleControl();
        }
    }

    void ToggleControl()
    {
        hasControl = !hasControl;
        if (hasControl)
            OnPlayerUnfreeze?.Invoke();
        else
            OnPlayerFreeze?.Invoke();
    }
}
