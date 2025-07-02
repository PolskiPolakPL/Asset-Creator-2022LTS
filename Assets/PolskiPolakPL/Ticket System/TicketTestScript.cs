using System.Collections.Generic;
using UnityEngine;

public class TicketTestScript : MonoBehaviour
{
    Ticket scpTicket;
    Ticket dClassTicket;
    Ticket scientist;
    Ticket mtfSpawn;
    Ticket ciSpawn;
    List<Ticket> targetTicketGroup;
    // Start is called before the first frame update
    void Start()
    {
        scpTicket = new Ticket(0, "SCP");
        dClassTicket = new Ticket(0, "D-class", 1);
        scientist = new Ticket(0, "Scientist", 1, 2);
        mtfSpawn = new Ticket(1, "mtfSpawnEvent", 0, 1);
        ciSpawn = new Ticket(1, "ciSpawnEvent", 0, 1);

        targetTicketGroup = Ticket.GetTicketsInGroup(0);
        DisplayAllTicketsFromGroup(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            scpTicket.GainTicket();
            DisplayTicketInfo(scpTicket);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            scpTicket.LooseTicket();
            DisplayTicketInfo(scpTicket);
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Ticket ticket = Ticket.GetRandomTicket(targetTicketGroup);
            DisplayTicketInfo(ticket);
        }
    }

    void DisplayAllTicketsFromGroup(int groupID)
    {
        List<Ticket> targetList = Ticket.GetTicketsInGroup(groupID);
        Debug.Log($"All Tickets from group {groupID}:");
        foreach (Ticket ticket in targetList)
        {
            Debug.Log($"{ticket.name} has {ticket.value} tickets ({Ticket.GetProbability(ticket)}% chanse)");
        }
    }

    void DisplayTicketInfo(Ticket ticket)
    {
        Debug.Log($"{ticket.name} has {ticket.value} tickets ({Ticket.GetProbability(ticket)}% chanse)");
    }
}

public enum TicketGroup
{
    PLAYER_CLASS,
    GAME_EVENTS
}