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
    public void SetVSync(bool isOn)
    {
        if (isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
    }

    public void SetAntiAliasing(int aaMode)
    {
        switch (aaMode)
        {
            case 0:
                {
                    QualitySettings.antiAliasing = 0;
                }
                break;

            case 1:
                {
                    QualitySettings.antiAliasing = 2;
                }
                break;

            case 2:
                {
                    QualitySettings.antiAliasing = 4;
                }
                break;

            case 3:
                {
                    QualitySettings.antiAliasing = 8;
                }
                break;

            default:
                {
                    Debug.LogWarning($"AntiAliasing = {aaMode} is invalid! Use 0,1,2 or 3. Higher the mode, the more precise AA");
                }
                break;
        }
    }
}
