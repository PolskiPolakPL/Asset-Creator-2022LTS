using System.Collections.Generic;
using UnityEngine;

public class ExampleEvent : MonoBehaviour
{
    [SerializeField] List<GameObject> objectsToAppear = new List<GameObject>();
    public void TriggerEvent()
    {
        Debug.Log("Random Event Triggered!");
    }

    public void Appear()
    {
        if(objectsToAppear.Count > 0)
        {
            foreach(GameObject obj in objectsToAppear)
            {
                obj.SetActive(true);
            }
        }
    }

    public void ChangeColor()
    {
        if (objectsToAppear.Count > 0)
        {
            int red = Random.Range(0, 200);
            int blue = Random.Range(0, 200);
            int green = Random.Range(0, 200);
            Debug.Log($"New Color: {red}, {green}, {blue}");
        }
    }

    public void Disappear()
    {
        if (objectsToAppear.Count > 0)
        {
            foreach (GameObject obj in objectsToAppear)
            {
                obj.SetActive(false);
            }
        }
    }
}
