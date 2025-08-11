using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance { get; private set; }

    public static string gameVersion { get; private set; }
    public static DeviceType deviceType { get; private set; }
    public static bool isTouchSupported { get; private set; }

    private void Awake()
    {
        if (Instance && Instance != this)
            Destroy(this);
        else
            Instance = this;

        Initialize();
    }

    void Initialize()
    {
        deviceType = SystemInfo.deviceType;
        isTouchSupported = Input.touchSupported;
        gameVersion = Application.version;
        SetFPSLimit(startingFPS);
    }

    [Min(1)]
    [SerializeField] int minFPSCount = 30;
    [SerializeField] int startingFPS = 60;

    public void SetCursorLockState(int lockStateID)
    {
        switch (lockStateID)
        {
            case 0:
                {
                    Cursor.lockState = CursorLockMode.None;
                }break;

            case 1:
                {
                    Cursor.lockState = CursorLockMode.Locked;
                }
                break;

            case 2:
                {
                    Cursor.lockState = CursorLockMode.Confined;
                }
                break;

            default:
                {
                    Debug.LogWarning($"LockStateID = {lockStateID} is invalid!");
                }
                break;
        }
    }

    public void SetFPSLimit(float value)
    {
        if (value < minFPSCount)
        {
            Debug.LogWarning($"FPS limit cannot be below {minFPSCount}! You can change required minimum value in AppManager Instance.");
            return;
        }
        Application.targetFrameRate = Mathf.RoundToInt(value);
    }

    public int GetFPSLimit()
    {
        return Application.targetFrameRate;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
