using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public RandomTicket ticket;
    // Start is called before the first frame update
    void Start()
    {
        ticket = new RandomTicket("TagTicket",10,10);
        Debug.Log($"{gameObject.name}'s current '{ticket.name}' tickets = {ticket.value}");
    }
}
