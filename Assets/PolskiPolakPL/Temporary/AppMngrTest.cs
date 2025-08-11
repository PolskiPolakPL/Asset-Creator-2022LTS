using UnityEngine;

public class AppMngrTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Device Information:" +
            $"DeviceType: {AppManager.deviceType.ToString()} \t Touch Supported: {AppManager.isTouchSupported} \t Current FPS Limit: {AppManager.Instance.GetFPSLimit()}");
    }
}
