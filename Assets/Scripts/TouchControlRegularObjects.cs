using UnityEngine;
using System.Collections;

public class TouchControlRegularObjects : MonoBehaviour {

    // Use this for initialization

    public int swipeDistance;

    public int speed;
    public float horizontalSpeed;
    public string state;
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
               
                    if (hit.collider.gameObject == gameObject)
                    {
                      
                        state = "swipeStart";
                    }
                
            }
        }
        else if (!Input.GetMouseButton(0) && state == "swipeStart")
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
                Vector3 direction = touchFinalLocation - currentPostion;
                direction.z = 1;
                
                if (direction.y > 0.6f)
                 direction.y = 0.6f;
                if (direction.y < 0.0f)
                  direction.y = 0.0f;
                
                if(direction.x < -0.55f)
                {
                    direction.x = -0.55f;
                }
                if(direction.x > 0.6f)
                {
                    direction.x = 0.6f;
                }
              
               
                direction.Normalize();
                int magnitudeOfForce = speed;
                GetComponent<Rigidbody>().velocity = direction * (magnitudeOfForce + (int)touchDiffrenceY);
                gameObject.GetComponent<Rigidbody>().useGravity = true;
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


   public void handleCollisions(GameObject ojbect)
    {

       
     state = "stuck";
     gameObject.GetComponent<Rigidbody>().useGravity = false;
     GetComponent<Rigidbody>().velocity = Vector3.zero;
     GetComponent<Collider>().enabled = false;
     gameObject.transform.SetParent(ojbect.transform);
     GetComponent<Rigidbody>().freezeRotation = true;

    }
}
