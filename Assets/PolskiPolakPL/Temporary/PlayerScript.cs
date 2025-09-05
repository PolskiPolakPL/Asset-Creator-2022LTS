using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public RandomTicket ticket;
    public int tagTimes = 0;
    // Start is called before the first frame update
    void Awake()
    {
        ticket = new RandomTicket("TagTicket",1,0);
        Debug.Log($"{gameObject.name}'s current '{ticket.name}' tickets = {ticket.value}");
    }
}
