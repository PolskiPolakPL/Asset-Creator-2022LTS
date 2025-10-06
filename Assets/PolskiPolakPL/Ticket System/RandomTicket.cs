using UnityEngine;
using System;

public class RandomTicket
{
    public string name { get; private set; }
    public int Weight { get; private set; }
    public int MinWeight { get; private set; }
    public int MaxWeight { get; private set; }
    /// <summary>
    /// A public Int value for tracking how many times ticket was missed.
    /// </summary>
    public int misses = 0;


    public RandomTicket(int value = 1, int minValue = 1, int maxValue = -1)
    {
        SetName("Unnamed");
        SetMinWeight(minValue);
        SetMaxWeight(maxValue);
        SetWeight(value);
    }

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
        SetMinWeight(minValue);
        SetMaxWeight(maxValue);
        SetWeight(value);
    }

    public void SetName(string newName)
    {
        if (newName == "")
            throw new ArgumentException($"[{this}]: New name cannot be empty!");
        else
            name = newName;
    }
    public void SetWeight(int newValue)
    {
        if (MaxWeight == -1)
            Weight = newValue;
        else
            Weight = Mathf.Min(MaxWeight, newValue);
        Weight = Mathf.Max(MinWeight, Weight);
    }
    public void SetMinWeight(int newValue)
    {
        if (newValue < 0)
            throw new ArgumentOutOfRangeException($"[{this}]: New min value cannot be below 0!");
        else
            MinWeight = newValue;
    }
    public void SetMaxWeight(int newValue)
    {
        if (newValue < -1)
            throw new ArgumentOutOfRangeException($"[{this}]: New max value cannot be below -1!");
        else
            MaxWeight = newValue;
    }
    public void Add(int amount)
    {
        if (amount < 0 || Weight+amount < 0)
        {
            Debug.LogWarning($"Specified amount ({amount}) cannot be below 0.");
            return;
        }
        SetWeight(Weight + amount);
    }
    public void Subtract(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning($"Specified amount ({amount}) cannot be below 0.");
            return;
        }
        SetWeight(Weight - amount);
    }
}
