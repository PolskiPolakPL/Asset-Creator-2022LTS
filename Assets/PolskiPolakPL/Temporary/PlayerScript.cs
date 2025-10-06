using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public RandomTicket tagTicket;
    public int tagTimes = 0;
    // Start is called before the first frame update
    void Awake()
    {
        tagTicket = new RandomTicket(1,0);
        Debug.Log($"{gameObject.name}'s current '{tagTicket.name}' tickets = {tagTicket.Weight}");
    }
}
