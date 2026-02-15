using System;
using UnityEngine;

public class VitalsMetric
{
    #region Fields and Events

    public float Min { get; private set; }
    public float Max { get; private set; }
    public float Current { get; private set; }

    //Events
    public event Action OnDrained;
    public event Action OnFilled;
    public event Action OnGained;
    public event Action OnLost;
    public event Action OnValueChanged;

    #endregion

    #region Constructors


    //float
    public VitalsMetric(float maxValue)
    {
        Min = 0;
        Max = maxValue;
        Current = maxValue;
    }

    public VitalsMetric(float minValue, float maxValue)
    {
        Min = minValue;
        Max = maxValue;
        Current = maxValue;
    }

    public VitalsMetric(float minValue, float maxValue, float startingValue)
    {
        Min = minValue;
        Max = maxValue;
        Current = startingValue;
    }

    #endregion

    #region Public Methods
    /// <summary>
    /// Increases current stat within it's limits.
    /// </summary>
    /// <param name="amount">How much you add to stat</param>
    /// <param name="notifyOnFilledEvent">Fire <c>OnFilled</c> event when Max value reached?</param>
    public void Gain(float amount, bool notifyOnFilledEvent = true)
    {
        GainWithoutNotify(amount);
        OnGained?.Invoke();
        if (Current == Min && notifyOnFilledEvent)
            OnFilled?.Invoke();
    }

    /// <summary>
    /// Decreases current stat within it's limits.
    /// </summary>
    /// <param name="amount">how much you subtract from stat</param>
    /// <param name="notifyOnDrainedEvent">Fire <c>OnDrained</c> event when Min value Reached?</param>
    public void Loose(float amount, bool notifyOnDrainedEvent = true)
    {
        LooseWithoutNotify(amount);
        OnLost?.Invoke();
        if(Current==Min && notifyOnDrainedEvent)
            OnDrained?.Invoke();
    }

    /// <summary>
    /// Increases current stat value within the limits.
    /// </summary>
    /// <param name="amount">increase amount</param>
    public void GainWithoutNotify(float amount)
    {
        //calculates new value
        float newIntVal = Current + amount;
        Current = Mathf.Min(newIntVal, Max);
    }

    /// <summary>
    /// Decreases current stat value within the limits. Doesn't invove <c>OnValueChange</c> nor <c>OnGain</c> Action.
    /// </summary>
    /// <param name="amount">decrease amount</param>
    public void LooseWithoutNotify(float amount)
    {
        //calculates new value
        float newVal = Current - amount;
        Current = Mathf.Max(newVal, Max);
    }

    #endregion

}
