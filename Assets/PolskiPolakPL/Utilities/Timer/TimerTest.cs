using PolskiPolakPL.Utils;
using TMPro;
using UnityEngine;

public class TimerTest : MonoBehaviour
{
    [SerializeField] TMP_Text timeTextField;
    [SerializeField] TMP_Text remainingTextField;
    [SerializeField] TMP_Text passedTextField;
    [SerializeField] float time = 1.5f;
    Timer timer;


    [Range(10,600)][SerializeField] int FPS = 60;

    //system time
    float sysTime;

    private void Awake()
    {
        timer = new Timer(time);

        if (remainingTextField)
            timer.OnTick += UpdateRemaining;

        if(passedTextField)
            timer.OnTick += UpdatePassed;

        timer.OnFinish += GetSystemTime;
    }

    private void Start()
    {
        UpdateTimeText();
    }

    // Update is called once per frame
    void Update()
    {
        timer.Tick(Time.deltaTime);
        Application.targetFrameRate = FPS;
    }

    void UpdatePassed()
    {
        passedTextField.text = "PASSED: " + timer.SecondsPassed.ToString();
    }
    void UpdateRemaining()
    {
        remainingTextField.text = "REMAIN: " + timer.RemaningSeconds.ToString();
    }

    void UpdateTimeText()
    {
        timeTextField.text = "Time: " + time.ToString();
    }

    int GetMiliseconds()
    {
        return System.DateTime.Now.Millisecond;
    }

    void GetSystemTime()
    {
        sysTime = (float)GetMiliseconds();
        Debug.Log(sysTime.ToString());
    }

    private void OnDestroy()
    {
        if (remainingTextField)
            timer.OnTick -= UpdateRemaining;
        if (passedTextField)
            timer.OnTick -= UpdatePassed;
        timer.OnFinish -= GetSystemTime;
    }
}
