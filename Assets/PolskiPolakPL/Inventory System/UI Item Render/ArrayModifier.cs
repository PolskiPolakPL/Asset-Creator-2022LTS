using UnityEngine;

public class ArrayModifier : MonoBehaviour
{
    [SerializeField][Min(1)] int arraySize = 1;
    GameObject targetGO;
    int prevSize = 0;
    GameObject[][] objectArray;

    private void OnValidate()
    {
        //attaches and hides the template object
        if (!targetGO)
        {
            targetGO = gameObject;
            targetGO.SetActive(false);
        }

        // Skip OnValidate() if arraySize has't changed
        if (arraySize == prevSize)
            return;

        //Define if array got bigger or smaller
        if (HasArrayGrew())
        {

        }
        else
        {

        }
        prevSize = arraySize;

    }

    bool HasArrayGrew()
    {
        if(prevSize<arraySize)
            return true;
        else return false;
    }

    void GrowArray()
    {
        for (int i = 0; i < arraySize; i++)
        {
            for(int j = 0; j < arraySize; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                //Create Duplicate
            }
        }
    }


}
