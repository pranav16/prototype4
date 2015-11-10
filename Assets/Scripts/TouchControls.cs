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

 
    
  
    


    void Start()
    {
     
        state = "init";
      
        
    }

    // Update is called once per frame
   public void Update()
    {

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
                if(direction.y < -0.6f)
                {
                    direction.y = 0.0f;
                }

                if(direction.y > 0.5f)
                {
                    direction.y = 0.5f;
                }

                direction.Normalize();
                int magnitudeOfForce = speed;
                Rigidbody[] bodies = gameObject.GetComponentsInChildren<Rigidbody>();
                for (int i = 0; i < bodies.Length; i++)
                {
                    bodies[i].velocity = direction * magnitudeOfForce;
                }
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


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            state = "stuck";
            Rigidbody[] bodies = gameObject.GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < bodies.Length; i++)
            {
                bodies[i].velocity = Vector3.zero; ;
            }
           
            gameObject.transform.SetParent(collision.collider.gameObject.transform);
        }
    }
}
