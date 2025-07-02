using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class TicketsManager
{
    public static List<Ticket> TicketsList = new List<Ticket>();

    public static List<Ticket> GetTicketsInGroup(int groupID)
    {
        List<Ticket> tickets = new List<Ticket>();
        foreach (Ticket ticket in TicketsList)
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

        int poolSize = TicketsManager.CoutAllTickets(tickets);
        int randomInt = UnityEngine.Random.Range(1, poolSize);

        foreach (Ticket ticket in tickets)
        {
            randomInt -= ticket.value;
            if(randomInt <= 0)
            {
                randomTicket = ticket;
                break;
            }
        }
        return randomTicket;
    }

    public static float GetProbability(Ticket ticket)
    {
        List<Ticket> pool = TicketsManager.GetTicketsInGroup(ticket.groupId);
        int poolSize = TicketsManager.CoutAllTickets(pool);
        float prob = Mathf.Floor(((float)ticket.value / poolSize) * 10000)/100;

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
