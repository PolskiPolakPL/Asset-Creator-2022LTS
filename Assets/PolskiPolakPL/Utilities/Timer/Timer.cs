using System;
namespace PolskiPolakPL.Utils
{
    /// <summary>
    /// Timer class from tutorial, extended by PolskiPolakPL. You can find original tutorial
    /// <seealso href="https://youtu.be/pRjTM3pzqDw">here</seealso>
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// Remaning time in seconds.
        /// </summary>
        public float RemaningSeconds { get; private set; }

        /// <summary>
        /// Time passed in seconds.
        /// </summary>
        public float SecondsPassed { get; private set; } = 0;

        private bool looping = true;

        /// <summary>
        /// Timer Action event invoked at the end of counting time.
        /// </summary>
        public event Action OnFinish;

        /// <summary>
        /// Timer Action event invoked every <c>Tick()</c>.
        /// </summary>
        public event Action OnTick;

        /// <summary>
        /// Timer Action event invoked whet it's duration is changed.
        /// </summary>
        public event Action OnChangeTime;

        public event Action OnReset;

        private float time;

        /// <summary>
        /// Constructor for Timer class.
        /// </summary>
        /// <param name="time">Duration of the timer in seconds</param>
        /// <param name="loop">Controlls if Timer is looping. Default = <c>true</c></param>
        public Timer(float time, bool loop = true)
        {
            this.time = time;
            RemaningSeconds = time;
            looping = loop;
        }

        /// <summary>
        /// Changes base duration of the Timer.
        /// </summary>
        /// <param name="time">sets new duration</param>
        /// <param name="reset">reserts remaning time back to the beginning. Default = <c>true</c></param>
        public void Set(float time, bool reset = true, bool invokeResetEvent = false)
        {
            SetSilent(time, reset, invokeResetEvent);
            OnChangeTime?.Invoke();
        }

        public void SetSilent(float time, bool reset = true, bool invokeResetEvent = false)
        {
            this.time = time;
            if (reset)
                Reset(invokeResetEvent);
        }

        public void Reset(bool invokeEvent = true)
        {
            RemaningSeconds = time;
            SecondsPassed = 0;
            if(invokeEvent)
                OnReset?.Invoke();
        }

        /// <summary>
        /// Method used to move time one tick. Recommended use in <c>Update()</c> or <c>FixedUpdate()</c> methods.
        /// </summary>
        /// <param name="deltaTime">time difference between ticks</param>
        /// <param name="invokeEvent">Controlls if Timer invokes <c>OnTimerChanged</c> Action. Deafault = <c>false</c></param>
        public void Tick(float deltaTime, bool invokeEvent = true)
        {
            if(RemaningSeconds == 0)
            {
                if (looping)
                    Reset();
                return;
            }
            RemaningSeconds -= deltaTime;
            SecondsPassed += deltaTime;
            if(invokeEvent)
                OnTick?.Invoke();
            if(CheckForTimerEnd())
            {
                RemaningSeconds = 0;
                OnFinish?.Invoke();
            }
        }

        private bool CheckForTimerEnd()
        {
            if(RemaningSeconds > 0)
                return false;
            return true;
        }
    }
}
