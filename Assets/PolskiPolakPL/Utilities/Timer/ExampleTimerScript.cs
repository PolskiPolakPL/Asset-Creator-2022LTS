using UnityEngine;
// Recommended using this namespace
using PolskiPolakPL.Utils;

public class ExampleTimerScript : MonoBehaviour
{
    // You can use Timer without using namespace if you want.
    PolskiPolakPL.Utils.Timer timerWithoutNamespace;

    // Define timer duration and loop boolean (recommended)
    [SerializeField] float timerDuration = 1;
    [SerializeField] bool loopTimer = false;

    // You can create multiple instances of the same Timer class.
    Timer defaultTimer, secondTimer;
    void Start()
    {
        // Default timer construction with minimum of required arguments
        defaultTimer = new Timer(timerDuration);

        // Another Timer with different time and additional arguments
        secondTimer = new Timer(timerDuration+3,loopTimer);
        

        // Subscribe your methods to right Actions.
        defaultTimer.OnTimerElapsed += ExampleMethod;
        defaultTimer.OnTimerTick += SeeTick;

        secondTimer.OnTimerElapsed += ExampleMethod2;
        secondTimer.OnTimerTick += SeeTick;

        // If you want an evet to occour at start, you have to skip first elapse
        defaultTimer.Tick(timerDuration);
    }

    // Here you can Tick the timers.
    void Update()
    {
        defaultTimer.Tick(Time.deltaTime);
        secondTimer.Tick(1,true); // here Timer will invoke `OnTimerTick` Action
    }

    // Your example methods called by Timer (Cannot have parameters)
    void ExampleMethod()
    {
        Debug.Log("ExampleMethod called!");
    }

    void ExampleMethod2()
    {
        Counter(3); // you can use methods with parameters inside subscribed method
    }

    void Counter(int countAmount)
    {
        for (int i = 0; i < countAmount; i++)
        {
            Debug.Log($" - - - {i}");
        }
    }

    void SeeTick()
    {
        Debug.Log("tick");
    }
}
