using UnityEngine;
using PolskiPolakPL.Utils;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] float refreshTime = 0.2f;
    TMP_Text fpsTextField;
    Timer timer;

    int currentFPS = 0;
    // Start is called before the first frame update
    void Start()
    {
        fpsTextField = GetComponent<TMP_Text>();
        timer = new Timer(refreshTime);
        timer.OnTimerElapsed += RefreshCounter;
        timer.Tick(refreshTime);
    }

    // Update is called once per frame
    void Update()
    {
        timer.Tick(Time.deltaTime);
    }
    void MeasureFPS()
    {
        currentFPS = Mathf.RoundToInt(1/Time.deltaTime);
    }

    void RefreshCounter()
    {
        MeasureFPS();
        fpsTextField.text = $"{currentFPS} FPS";
    }

    private void OnDestroy()
    {
        timer.OnTimerElapsed -= RefreshCounter;
    }
}
