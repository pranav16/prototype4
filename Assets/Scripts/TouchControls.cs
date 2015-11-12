using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchControls : MonoBehaviour
{

    // Use this for initialization

    public int swipeDistance;
 
    public int speed;
    public float horizontalSpeed;
    public string state;
    private BoxCollider Hitcollider;
    private float timerBeforeDestruction;
    public float maxObjectLife;

    void Start()
    {
     
        state = "init";
        timerBeforeDestruction = 0.0f;
     
      
        
    }

    // Update is called once per frame
   public void Update()
    {
        timerBeforeDestruction++;
        if (state == "stuck")
            return;
        if (timerBeforeDestruction >= maxObjectLife)
            Destroy(gameObject);
        if (Input.GetMouseButton(0) && state == "init")
        {
            Vector3 mousePostion = Input.mousePosition;
            mousePostion.z = 10;
            Ray ray = Camera.main.ScreenPointToRay(mousePostion);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                BoxCollider[] colliders = gameObject.GetComponentsInChildren<BoxCollider>();
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (hit.collider == colliders[i])
                    {
                        Hitcollider = colliders[i];
                        state = "swipeStart";
                    }
                }
            }
        }
        else if (!Input.GetMouseButton(0)  && state == "swipeStart")
        {
            Vector3 touchFinalLocation = Input.mousePosition;
            touchFinalLocation.z = 10;
            touchFinalLocation = Camera.main.ScreenToWorldPoint(touchFinalLocation);
            double touchDiffrenceY = Mathf.Abs(transform.position.y - touchFinalLocation.y);
            double touchDiffrenceX = Mathf.Abs(transform.position.x - touchFinalLocation.x);
            if (touchDiffrenceX >= swipeDistance || touchDiffrenceY >= swipeDistance)
            {
              
                touchFinalLocation.Normalize();
                Vector3 currentPostion = transform.position;
                currentPostion.Normalize();
                Vector3 direction= touchFinalLocation - currentPostion;
                direction.z = 1;
                if (direction.y < 0.0f)
                {
                    direction.y = 0.0f;
                }

                if (direction.y > 0.5f)
                {
                    direction.y = 0.5f;
                }

                if (direction.x < -0.55f)
                {
                    direction.x = -0.55f;
                }
                if (direction.x > 0.6f)
                {
                    direction.x = 0.6f;
                }
                direction.Normalize();
                int magnitudeOfForce = speed;
                Hitcollider.gameObject.GetComponent<Rigidbody>().velocity = direction * magnitudeOfForce;
                Hitcollider.gameObject.GetComponent<Rigidbody>().useGravity = true;
               state = "moving";
            }
        }
        if (state == "init" && !Input.GetMouseButton(0))
        {
            Vector3 position = gameObject.transform.position;
            position.x += horizontalSpeed * Time.deltaTime;
            gameObject.transform.position = position;
            state = "init";
        }
       
    }
    public void setState(string state)
    {
        this.state = state;
    }

    public string getState()
    {
        return state;
    }


   public void handleCollisions(GameObject Planeobject)
    {
        
        state = "stuck";
        BoxCollider[] colliders = gameObject.GetComponentsInChildren<BoxCollider>();
        Rigidbody[] bodies = gameObject.GetComponentsInChildren<Rigidbody>();
        //Hitcollider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
            for (int i = 0; i < bodies.Length; i++)
            {
            bodies[i].velocity = Vector3.zero;
            bodies[i].isKinematic = true;
           }

    }
}
