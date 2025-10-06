using System.Collections.Generic;
using PolskiPolakPL.Utils;
using UnityEngine;

public class ExampleScript : MonoBehaviour
{
    // Serialized fields allow you to tweak these ticket counts directly in the Unity Inspector
    [SerializeField] int redTicketsCount = 3;
    [SerializeField] int greenTicketsCount = 4;
    [SerializeField] int blueTicketsCount = 3;

    // Instances of RandomTicket representing different ticket colors
    RandomTicket redTicket;
    RandomTicket greenTicket;
    RandomTicket blueTicket;

    // Total weight (sum of all ticket weights) used for random selection
    int poolSize = 0;

    void Start()
    {
        redTicket = new RandomTicket(redTicketsCount,2);
        greenTicket = new RandomTicket("Green Ticket",greenTicketsCount);
        blueTicket = new RandomTicket(blueTicketsCount);
        poolSize = redTicket.Weight + greenTicket.Weight + blueTicket.Weight;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Demo1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Demo2();
        }
    }

    // Demo1: Shows that you don't need a list to make ticket selection work
    void Demo1()
    {
        // Pick a random value in range of the total pool size
        int randomIndex = Random.Range(0, poolSize);
        float probability = 0;

        // Check if the random value falls within the red ticket's weight range
        randomIndex -= redTicket.Weight;
        if(randomIndex < 0)
        {
            probability = redTicket.Weight/(float)poolSize;
            DisplayRed($"Red ticket with name: '{redTicket.name}' was chosen with probability = {Mathf.Round(probability*10000)/100}");
            return;
        }

        // Check if it falls within the green ticket's range
        randomIndex -= greenTicket.Weight;
        if (randomIndex < 0)
        {
            probability = greenTicket.Weight / (float)poolSize;
            DisplayGreen($"Green ticket with name: '{greenTicket.name}' was chosen with probability = {Mathf.Round(probability * 10000) / 100}");
        }
        else // Otherwise, the blue ticket wins
        {
            probability = blueTicket.Weight / (float)poolSize;
            DisplayBlue(blueTicket.ToString());
        }
    }

    // Demo2: Demonstrates selection using a list and dynamic weight adjustments
    void Demo2()
    {
        // Create a list of tickets to iterate through
        List<RandomTicket> ticketsList = new List<RandomTicket>();
        ticketsList.Add(redTicket);
        ticketsList.Add(greenTicket);
        ticketsList.Add(blueTicket);

        // Recalculate pool size in case weights have changed. You can also use a loop of the List elements.
        poolSize = redTicket.Weight + greenTicket.Weight + blueTicket.Weight;

        // Randomly select one ticket based on its weight
        int randomIndex = Random.Range(0, poolSize);

        foreach (RandomTicket ticket in ticketsList)
        {
            randomIndex -= ticket.Weight;

            if (randomIndex < 0)// The chosen ticket
            {
                DisplayChoosen(ticket,ticket.ToString());

                // Reset its weight and miss counter
                ticket.SetWeight(ticket.MinWeight);
                ticket.misses = 0;
                break;
            }
            else // Ticket was not chosen — increase its miss counter and slightly raise its weight
            {
                ticket.misses++;
                ticket.Add(1);
            }
        }
    }

    // Helper methods
    #region metody pomocnicze

    // Displays the chosen ticket using the proper color format
    void DisplayChoosen(RandomTicket ticket, string message)
    {
        if(ticket == redTicket)
            DisplayRed(message);
        else if(ticket == greenTicket)
            DisplayGreen(message);
        else
            DisplayBlue(message);
    }

    // Display methods for colored console output in Unity
    void DisplayRed(string message)
    {
        Debug.Log($"<color=#ff0000>{message}</color>");
    }

    void DisplayGreen(string message)
    {
        Debug.Log($"<color=#00ff00>{message}</color>");
    }

    void DisplayBlue(string message)
    {
        Debug.Log($"<color=#0000ff>{message}</color>");
    }
    #endregion
}
