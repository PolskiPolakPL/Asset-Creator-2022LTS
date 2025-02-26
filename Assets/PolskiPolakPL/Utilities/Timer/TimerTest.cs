using UnityEngine;
using PolskiPolakPL.Utils;

public class TimerTest : MonoBehaviour
{
    [SerializeField] float timerDuration;

    Timer timer;
    float prevoiusDuration;
    void Start()
    {
        prevoiusDuration = timerDuration;
        timer = new Timer(timerDuration);
        timer.OnTimerElapsed += ExampleMethod;
        timer.OnTimerTick += LogTick;
        timer.OnDurationChanged += ShowNewValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(prevoiusDuration != timerDuration)
        {
            timer.ChangeDuration(timerDuration);
            prevoiusDuration = timerDuration;
        }
        timer.Tick(Time.deltaTime);
    }

    void ExampleMethod()
    {
        Debug.Log("Timer Elapsed!");
    }
    void LogTick()
    {
        Debug.Log(".");
    }
    void ShowNewValue()
    {
        Debug.Log($"Changed Timer duration to: {timerDuration}");
    }
}
