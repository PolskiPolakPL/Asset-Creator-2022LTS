using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    float targetFPS = 60;

    public void SetFPS(float value)
    {
        targetFPS = value;
        Application.targetFrameRate = (int)targetFPS;
    }

    public void LimitFPS(bool value)
    {
        if (value)
            Application.targetFrameRate = (int)targetFPS;
        else
            Application.targetFrameRate = 600;
    }
}
