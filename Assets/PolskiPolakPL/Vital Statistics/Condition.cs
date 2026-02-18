using System;
using UnityEngine;

public class Condition
{
    #region Fields and Events

    //Fields
    public float MinVal { get; private set; }
    public float MaxVal { get; private set; }
    public float CurrentVal { get; private set; }

    //Events
    public event Action OnDrained;
    public event Action OnFilled;
    public event Action OnGained;
    public event Action OnLost;
    public event Action OnCurrentValChanged;
    public event Action OnMaxChanged;
    public event Action OnMinChanged;

    #endregion

    #region Constructors


    //float
    public Condition(float maxValue)
    {
        MinVal = 0;
        MaxVal = maxValue;
        CurrentVal = maxValue;
    }

    public Condition(float minValue, float maxValue)
    {
        MinVal = minValue;
        MaxVal = maxValue;
        CurrentVal = maxValue;
    }

    public Condition(float minValue, float maxValue, float startingValue)
    {
        MinVal = minValue;
        MaxVal = maxValue;
        CurrentVal = startingValue;
    }

    #endregion

    #region Public Methods

    public void SetMaxValue(float value)
    {
        if (value > MinVal)
        {
            MaxVal = value;
            GainWithoutNotify(0);
            OnMaxChanged();
        }
        else
            Debug.LogWarning("New MAX value must be above current MIN value.");
    }

    public void SetMinValue(float value)
    {
        if (value < MaxVal)
        {
            MinVal = value;
            LooseWithoutNotify(0);
            OnMinChanged();
        }
        else
            Debug.LogWarning("New MIN value must below current MAX value.");
    }

    public void SetCurrentValue(float value)
    {
        CurrentVal=value;
        OnCurrentValChanged?.Invoke();
    }

    public float GetNormalizedValue()
    {
        return CurrentVal/MaxVal;
    }


    /// <summary>
    /// Increases current stat within its limits.
    /// </summary>
    /// <param name="amount">How much you add to stat</param>
    /// <param name="notifyOnFilledEvent">Fire <c>OnFilled</c> event when Max value reached?</param>
    public void Gain(float amount, bool notifyOnFilledEvent = true)
    {
        GainWithoutNotify(amount);
        OnGained?.Invoke();
        if (CurrentVal>=MaxVal && notifyOnFilledEvent)
            OnFilled?.Invoke();
    }

    /// <summary>
    /// Decreases current stat within its limits.
    /// </summary>
    /// <param name="amount">how much you subtract from stat</param>
    /// <param name="notifyOnDrainedEvent">Fire <c>OnDrained</c> event when Min value Reached?</param>
    public void Loose(float amount, bool notifyOnDrainedEvent = true)
    {
        LooseWithoutNotify(amount);
        OnLost?.Invoke();
        if(CurrentVal<=MinVal && notifyOnDrainedEvent)
            OnDrained?.Invoke();
    }

    /// <summary>
    /// Increases current stat value within the limits.
    /// </summary>
    /// <param name="amount">increase amount</param>
    public void GainWithoutNotify(float amount)
    {
        CurrentVal = Mathf.Min(CurrentVal + amount, MaxVal);
    }

    /// <summary>
    /// Decreases current stat value within the limits. Doesn't invove <c>OnValueChange</c> nor <c>OnGain</c> Action.
    /// </summary>
    /// <param name="amount">decrease amount</param>
    public void LooseWithoutNotify(float amount)
    {
        //calculates new value
        CurrentVal = Mathf.Max(CurrentVal - amount, MinVal);
    }

    #endregion

}
