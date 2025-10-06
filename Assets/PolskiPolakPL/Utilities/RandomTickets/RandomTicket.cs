using UnityEngine;
using System;

namespace PolskiPolakPL.Utils
{
    /// <summary>
    /// Represents a single weighted ticket used for probabilistic random selection.
    /// </summary>
    public class RandomTicket
    {
        /// <summary>
        /// The display name of this ticket.
        /// </summary>
        public string name { get; private set; }
        /// <summary>
        /// The current weight (number of "entries") this ticket holds.
        /// The higher the weight, the higher the probability of being selected.
        /// </summary>
        public int Weight { get; private set; }
        /// <summary>
        /// The minimum possible weight value.
        /// Ensures that ticket weight cannot fall below this limit.
        /// </summary>
        public int MinWeight { get; private set; }

        /// <summary>
        /// The maximum possible weight value.
        /// If set to -1, the weight limit is disabled.
        /// </summary>
        public int MaxWeight { get; private set; }
        /// <summary>
        /// Tracks how many consecutive rounds this ticket has been missed (not selected).
        /// Can be used for adjusting weight dynamically. (You have to set it manually)
        /// </summary>
        public int misses = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomTicket"/> class with default parameters.
        /// </summary>
        /// <param name="value">Initial weight value.</param>
        /// <param name="minValue">Minimum allowed weight value.</param>
        /// <param name="maxValue"> Maximum allowed weight value. If set to -1, the maximum limit is ignored.</param>
        public RandomTicket(int value = 1, int minValue = 1, int maxValue = -1)
        {
            SetName("Unnamed Ticket");
            SetMinWeight(minValue);
            SetMaxWeight(maxValue);
            SetWeight(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomTicket"/> class with a custom name and optional parameters.
        /// </summary>
        /// <param name="name">Display name of the ticket.</param>
        /// <param name="value">Initial weight value.</param>
        /// <param name="minValue">Minimum allowed weight value.</param>
        /// <param name="maxValue">Maximum allowed weight value. If set to -1, the maximum limit is ignored.</param>
        /// <exception cref="ArgumentException">Thrown when the provided name is empty.</exception>
        public RandomTicket(string name, int value = 1,int minValue=1, int maxValue = -1)
        {
            SetName(name);
            SetMinWeight(minValue);
            SetMaxWeight(maxValue);
            SetWeight(value);
        }

        /// <summary>
        /// Assigns a new name to this ticket.
        /// </summary>
        /// <param name="newName">The new name to assign.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="newName"/> is empty.</exception>
        public void SetName(string newName)
        {
            if (newName == "")
                throw new ArgumentException($"[{this}]: New name cannot be empty!");
            else
                name = newName;
        }

        /// <summary>
        /// Sets the current ticket weight and clamps it between <see cref="MinWeight"/> and <see cref="MaxWeight"/> (if applicable).
        /// </summary>
        /// <param name="newValue">The new weight value.</param>
        public void SetWeight(int newValue)
        {
            if (MaxWeight == -1)
                Weight = newValue;
            else
                Weight = Mathf.Min(MaxWeight, newValue);
            Weight = Mathf.Max(MinWeight, Weight);
        }

        /// <summary>
        /// Sets the minimum allowed ticket weight.
        /// </summary>
        /// <param name="newValue">New minimum value (must be >= 0).</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="newValue"/> is less than 0.</exception>
        public void SetMinWeight(int newValue)
        {
            if (newValue < 0)
                throw new ArgumentOutOfRangeException($"[{this}]: New min value cannot be below 0!");
            else
                MinWeight = newValue;
        }

        /// <summary>
        /// Sets the maximum allowed ticket weight.
        /// </summary>
        /// <param name="newValue">New maximum value. Use -1 to disable the limit.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="newValue"/> is less than -1.</exception>
        public void SetMaxWeight(int newValue)
        {
            if (newValue < -1)
                throw new ArgumentOutOfRangeException($"[{this}]: New max value cannot be below -1!");
            else
                MaxWeight = newValue;
        }

        /// <summary>
        /// Increases the ticket's weight by the specified amount, respecting <see cref="MaxWeight"/>.
        /// </summary>
        /// <param name="amount">The amount to add (must be non-negative).</param>
        public void Add(int amount)
        {
            if (amount < 0 || Weight+amount < 0)
            {
                Debug.LogWarning($"Specified amount ({amount}) cannot be below 0.");
                return;
            }
            SetWeight(Weight + amount);
        }

        /// <summary>
        /// Decreases the ticket's weight by the specified amount, respecting <see cref="MinWeight"/>.
        /// </summary>
        /// <param name="amount">The amount to subtract (must be non-negative).</param>
        public void Subtract(int amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning($"Specified amount ({amount}) cannot be below 0.");
                return;
            }
            SetWeight(Weight - amount);
        }

        /// <summary>
        /// Returns a human-readable string representation of the current <see cref="RandomTicket"/> instance.
        /// Useful for debugging and logging purposes.
        /// </summary>
        /// <returns>
        /// A formatted string containing the ticket's name, current weight (with min &amp; max), and misses count.
        /// </returns>
        public override string ToString()
        {
            return $"[RandomTicket: Name='{name}', Weight={Weight} (Min={MinWeight}, Max={MaxWeight}), Misses={misses}]";
        }
    }
}
