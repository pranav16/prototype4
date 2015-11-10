using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectsHandler : MonoBehaviour {

    // Use this for initialization
    public List<GameObject> listOfAvailableObjects;
    public int timeIntervalBetweenSpawn;
    private int intervalCounter;
    private List<GameObject> listOfThrowableObjects;
    void Start () {

        listOfThrowableObjects = new List<GameObject>();
        intervalCounter = 0;

    }

    // Update is called once per frame
    void Update()
    {

        intervalCounter++;
        if(intervalCounter >= timeIntervalBetweenSpawn || listOfThrowableObjects.Count == 0)
        {
            spawnNewElement();
            intervalCounter = 0;
        }

  
        for (int i = 0; i < listOfThrowableObjects.Count; i++)
        {
            TouchControls control = listOfThrowableObjects[i].GetComponent<TouchControls>();
            if (listOfThrowableObjects[i].transform.position.x > 4.0f  && control.getState() == "init")
            {

               GameObject objectToBeDestroyed = listOfThrowableObjects[i];
               listOfThrowableObjects.RemoveAt(i);
               Destroy(objectToBeDestroyed);
                
                continue;
            }
            if (listOfThrowableObjects[i].transform.position.z > 10.0f && control.getState() == "moving")
            {

                GameObject objectToBeDestroyed = listOfThrowableObjects[i];
                listOfThrowableObjects.RemoveAt(i);
                Destroy(objectToBeDestroyed);

                continue;
            }

      

        }


    }



    private void spawnNewElement()
    {

       int  index = Random.Range(0, listOfAvailableObjects.Count);
        Vector3 position = new Vector3(-4.0f, 0.0f, -8.02f);
        GameObject gobject = Instantiate(listOfAvailableObjects[index]);
        gobject.transform.position = position;
        listOfThrowableObjects.Add(gobject);
    }

}
