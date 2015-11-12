using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectsHandler : MonoBehaviour {

    // Use this for initialization
    public List<GameObject> listOfAvailableObjects;
    public int timeIntervalBetweenSpawn;
    private int intervalCounter;

    void Start () {

        
        intervalCounter = timeIntervalBetweenSpawn;

    }

    // Update is called once per frame
    void Update()
    {

        intervalCounter++;
        if(intervalCounter >= timeIntervalBetweenSpawn )
        {
            spawnNewElement();
            intervalCounter = 0;
        }

    }



    private void spawnNewElement()
    {

       int  index = Random.Range(0, listOfAvailableObjects.Count);
        Vector3 position = new Vector3(-4.0f, 0.0f, -8.02f);
        GameObject gobject = Instantiate(listOfAvailableObjects[index]);
        gobject.transform.position = position;
     
    }

}
