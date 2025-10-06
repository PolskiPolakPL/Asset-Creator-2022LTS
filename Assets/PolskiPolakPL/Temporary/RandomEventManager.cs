
using UnityEngine;

public class RandomEventManager : MonoBehaviour
{
    [SerializeField] int redCubeTickets;
    [SerializeField] int blueCubeTickets;
    [SerializeField] int noCubeTickets;
    [SerializeField] ExampleEvent exampleEventScript;
    RandomTicket redCubes;
    RandomTicket blueCubes;
    RandomTicket whiteCubes;

    int poolSize;
    int whiteCounter=0, blueCounter=0, redCounter=0;

    private void Awake()
    {
        redCubes = new RandomTicket(redCubeTickets);
        blueCubes = new RandomTicket(blueCubeTickets);
        whiteCubes = new RandomTicket(noCubeTickets);
    }
    // Start is called before the first frame update
    void Start()
    {
        poolSize = redCubes.Weight + blueCubes.Weight + whiteCubes.Weight;
        Debug.Log($"Random Events - Pool size:{poolSize}");
        float probability = redCubes.Weight/(float)poolSize;
        Debug.Log($"<color=#ff0000>Probability of red cubes = {Mathf.RoundToInt(probability * 100)}%</color>");
        probability = blueCubes.Weight / (float)poolSize;
        Debug.Log($"<color=#0000ff>Probability of blue cubes = {Mathf.RoundToInt(probability * 100)}%</color>");
        probability = whiteCubes.Weight / (float)poolSize;
        Debug.Log($"<color=#ffffff>Probability of white cubes = {Mathf.RoundToInt(probability * 100)}%</color>");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            float sum = redCounter + blueCounter + whiteCounter;
            Debug.Log($"SUM: {sum} | <color=#ff0000>{redCounter}({Mathf.RoundToInt((redCounter/sum)*100)})</color> \t <color=#0000ff>{blueCounter}({Mathf.RoundToInt((blueCounter / sum) * 100)})</color> \t <color=#ffffff>{whiteCounter}({Mathf.RoundToInt((whiteCounter / sum) * 100)})</color>");
        }
    }

    void PaintCubes(Color newColor)
    {
        foreach(GameObject cube in exampleEventScript.objectsToAppear)
        {
            cube.GetComponent<Renderer>().material.color = newColor;
        }
    }

    public Color PickRandomColor()
    {
        int randomIndex = Random.Range(1, poolSize+1);
        randomIndex -= redCubes.Weight;
        if (randomIndex <= 0)
        {
            redCounter++;
            return new Color(255, 0, 0);
        }
        randomIndex -= blueCubes.Weight;
        if (randomIndex <= 0)
        {
            blueCounter++;
            return new Color(0, 0, 255);
        }
        else
        {
            whiteCounter++;
            return new Color(255,255,255);
        }
    }

    public void ActivateRandomEvent()
    {
        PaintCubes(PickRandomColor());
    }
}
