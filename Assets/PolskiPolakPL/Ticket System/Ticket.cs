using System.Collections.Generic;
using UnityEngine;

public class Ticket
{

    private static List<Ticket> allTickets = new List<Ticket>();

    public int groupId { get; private set; }
    public string name { get; private set; }
    public int value { get; private set; }

    private int minValue;

    public Ticket(int groupId, string name)
    {
        this.groupId = groupId;
        this.name = name;
        minValue = 0;
        value = 1;
        Ticket.allTickets.Add(this);
    }
    public Ticket(int groupId, string name, int minValue)
    {
        this.groupId = groupId;
        this.name = name;
        this.minValue = minValue;
        value = minValue;
        Ticket.allTickets.Add(this);
    }
    public Ticket(int groupId, string name, int minValue, int value)
    {
        this.groupId = groupId;
        this.name = name;
        this.minValue = minValue;
        if(value<minValue)
            value = minValue;
        this.value = value;
        Ticket.allTickets.Add(this);
    }

    ~Ticket()
    {
        if(Ticket.allTickets.Count>0 && Ticket.allTickets.Contains(this))
            Ticket.allTickets.Remove(this);
    }

    public void ChangeName(string newName)
    {
        if(newName == "")
        {
            Debug.LogWarning("New name cannot be empty! Revoked change.");
            return;
        }
        this.name = newName;
    }

    public void ChangeGroup(int newGroupID)
    {
        this.groupId = newGroupID;
    }

    public void SetMinValue(int minValue)
    {
        this.minValue = minValue;
        if(value < minValue)
            value = minValue;
    }

    public void SetValue(int newValue)
    {
        value = newValue;
        if (value < minValue)
        {
            value = minValue;
            Debug.LogWarning($"New Ticket vaule ({newValue}) is below minimum({minValue})! Chenged current value to {value}.");
        }
    }

    public void GainTicket()
    {
        value++;
    }

    public void GainTicket(int amount)
    {
        value += amount;
    }

    public void LooseTicket()
    {
        value--;
    }
    public void LooseTicket(int amount)
    {
        value -= amount;
        if (value < 0)
            value = 0;
    }


    public static List<Ticket> GetTicketsInGroup(int groupID)
    {
        List<Ticket> tickets = new List<Ticket>();
        foreach (Ticket ticket in allTickets)
        {
            if (ticket.groupId == groupID)
                tickets.Add(ticket);
        }
        return tickets;
    }

    public static Ticket GetRandomTicket(List<Ticket> tickets)
    {
        tickets.Sort();
        Ticket randomTicket = null;

        int poolSize = Ticket.CoutAllTickets(tickets);
        int randomInt = UnityEngine.Random.Range(1, poolSize);

        foreach (Ticket ticket in tickets)
        {
            randomInt -= ticket.value;
            if (randomInt <= 0)
            {
                randomTicket = ticket;
                break;
            }
        }
        return randomTicket;
    }

    public static float GetProbability(Ticket ticket)
    {
        List<Ticket> pool = Ticket.GetTicketsInGroup(ticket.groupId);
        int poolSize = Ticket.CoutAllTickets(pool);
        float prob = Mathf.Floor(((float)ticket.value / poolSize) * 10000) / 100;

        return prob;
    }

    public static int CoutAllTickets(List<Ticket> tickets)
    {
        int ticketsCount = 0;

        foreach (Ticket ticket in tickets)
        {
            ticketsCount += ticket.value;
        }
        return ticketsCount;
    }
}