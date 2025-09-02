using UnityEngine;
using System;

public class RandomTicket
{
    public string name { get; private set; }
    public int value { get; private set; }
    public int minValue { get; private set; }
    public int maxValue { get; private set; }
    /// <summary>
    /// Creates new 'RandomTicket' object.
    /// </summary>
    /// <param name="name">Name of the ticket.</param>
    /// <param name="value">Current ticket value.</param>
    /// <param name="minValue">Minimum ticket value.</param>
    /// <param name="maxValue">Maximum ticket value. If <c>maxValue</c> is set to -1 then the limit is turned off.</param>
    /// <exception cref="ArgumentException"></exception>
    public RandomTicket(string name, int value = 1,int minValue=1, int maxValue = -1)
    {
        SetName(name);
        SetMinValue(minValue);
        SetMaxValue(maxValue);
        SetValue(value);
    }

    public void SetName(string newName)
    {
        if (newName == "")
            throw new ArgumentException($"[{this}]: New name cannot be empty!");
        else
            name = newName;
    }
    public void SetValue(int newValue)
    {
        if (maxValue == -1)
            value = newValue;
        else
            value = Mathf.Min(maxValue, newValue);
        value = Mathf.Max(minValue, value);
    }
    public void SetMinValue(int newValue)
    {
        if (newValue < 0)
            throw new ArgumentOutOfRangeException($"[{this}]: New min value cannot be below 0!");
        else
            minValue = newValue;
    }
    public void SetMaxValue(int newValue)
    {
        if (newValue < -1)
            throw new ArgumentOutOfRangeException($"[{this}]: New max value cannot be below -1!");
        else
            maxValue = newValue;
    }
    public void Add(int amount)
    {
        if (amount < 0 || value+amount < 0)
        {
            Debug.LogWarning($"Specified amount ({amount}) cannot be below 0.");
            return;
        }
        SetValue(value + amount);
    }
    public void Subtract(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning($"Specified amount ({amount}) cannot be below 0.");
            return;
        }
        SetValue(value - amount);
    }
}
