using UnityEngine;

public class FakeMultiplayerManagerScript : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;
    [SerializeField] Transform PlayersParent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ConnectNewPlayer()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Instantiate(PlayerPrefab, PlayersParent);
        }
    }
}
