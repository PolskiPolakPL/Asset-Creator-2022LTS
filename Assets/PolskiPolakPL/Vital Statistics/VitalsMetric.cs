using System;
using UnityEngine;

public class VitalsMetric
{
    #region Fields and Events

    public float Min { get; private set; }
    public float Max { get; private set; }
    public float Current { get; private set; }

    //Events
    public event Action OnEmpty;
    public event Action OnFill;
    public event Action OnGain;
    public event Action OnLoose;
    public event Action OnValueChange;

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
    /// Increases current stat value within the limits.
    /// </summary>
    /// <param name="amount">increase amount</param>
    /// <param name="notifyFillEvent">Should method invoke <c>OnFill Action</c> when the metric gets filled. True on default</param>
    public void Gain(float amount, bool notifyFillEvent = true)
    {
        //calculates new value
        float newIntVal = Current + amount;
        newIntVal = Mathf.Min(newIntVal, Max);

        //if new value is different
        if (newIntVal != Current)
        {
            if (notifyFillEvent)
                CheckForFilledEvent(newIntVal);
            Current = newIntVal;
            OnValueChange?.Invoke();
        }

        //Invoke OnGainEvent
        OnGain.Invoke();
    }

    /// <summary>
    /// Decreases current stat value within the limits.
    /// </summary>
    /// <param name="amount">decrease amount</param>
    /// <param name="notifyEmptyEvent">Should method invoke <c>OnEmpty Action</c> when the metric gets depleted. True on default</param>
    public void Loose(float amount, bool notifyEmptyEvent = true)
    {
        //calculates new value
        float val = Current - amount;
        val = Mathf.Max(val, Min);

        //if new value is different
        if (val != Current)
        {
            if (notifyEmptyEvent)
                CheckForEmptyEvent(Current);
            Current = val;
            OnValueChange?.Invoke();
        }

        //Invoke OnLooseEvent
        OnLoose?.Invoke();
    }

    /// <summary>
    /// Increases current stat value within the limits. Doesn't invove <c>OnValueChange</c> nor <c>OnGain</c> Action.
    /// </summary>
    /// <param name="amount">increase amount</param>
    /// <param name="notifyFillEvent">Should method invoke <c>OnFill Action</c> when the metric gets filled. True on default</param>
    public void GainWithoutNotify(float amount, bool notifyFillEvent = true)
    {
        //calculates new value
        float newIntVal = Current + amount;
        newIntVal = Mathf.Min(newIntVal, Max);

        //if new value is different
        if (newIntVal != Current)
        {
            if (notifyFillEvent)
                CheckForFilledEvent(newIntVal);
            Current = newIntVal;
        }
    }

    /// <summary>
    /// Decreases current stat value within the limits. Doesn't invove <c>OnValueChange</c> nor <c>OnGain</c> Action.
    /// </summary>
    /// <param name="amount">decrease amount</param>
    /// <param name="notifyEmptyEvent">Should method invoke <c>OnEmpty Action</c> when the metric gets depleted. True on default</param>
    public void LooseWithoutNotify(float amount, bool notifyEmptyEvent = true)
    {
        //calculates new value
        float val = Current - amount;
        val = Mathf.Max(val, Max);

        //if new value is different
        if (val != Current)
        {
            if (notifyEmptyEvent)
                CheckForEmptyEvent(Current);
            Current = val;
        }
    }

    #endregion

    #region Private Methods

    //float
    private void CheckForFilledEvent(float val)
    {
        if (val < Max)
            return;
        OnFill?.Invoke();
    }

    private void CheckForEmptyEvent(float val)
    {
        if (val > Min)
            return;
        OnEmpty?.Invoke();
    }

    #endregion

}
