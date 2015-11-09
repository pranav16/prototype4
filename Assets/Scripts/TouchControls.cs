using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchControls : MonoBehaviour
{

    // Use this for initialization

    public int swipeDistance;
    private Vector3 touchStartLocation;
    public int speed;
    public float horizontalSpeed;
    public string state;
 
    
  
    


    void Start()
    {
     
        state = "init";
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && state == "init")
        {
            Vector3 mousePostion = Input.mousePosition;
            mousePostion.z = 10;
            Ray ray = Camera.main.ScreenPointToRay(mousePostion);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    touchStartLocation = mousePostion;
                    state = "swipeStart";
                }
            }
        }
        else if (Input.GetMouseButton(0)  && state == "swipeStart")
        {
        
            double touchDiffrenceY = Mathf.Abs(touchStartLocation.y - Input.mousePosition.y);
            double touchDiffrenceX = Mathf.Abs(touchStartLocation.x - Input.mousePosition.x);
            if (touchDiffrenceX >= swipeDistance || touchDiffrenceY >= swipeDistance)
            {
                Vector3 touchFinalLocation = Input.mousePosition;
                touchFinalLocation.z = 10;
                touchStartLocation = touchFinalLocation;
                touchFinalLocation = Camera.main.ScreenToWorldPoint(touchFinalLocation);
                touchFinalLocation.Normalize();
                Vector3 currentPostion = transform.position;
                currentPostion.Normalize();
                Vector3 direction= touchFinalLocation - currentPostion;

                direction.z = 1;
                direction.Normalize();
                int magnitudeOfForce = speed;  
                gameObject.GetComponent<Rigidbody>().velocity = direction * magnitudeOfForce;
                state = "moving";
            }
        }
        else if (state != "moving" && state != "stuck" && !Input.GetMouseButton(0))
        {
            Vector3 position = gameObject.transform.position;
            position.x += horizontalSpeed * Time.deltaTime;
            gameObject.transform.position = position;
            state = "init";
        }



    }
    public void captureImage()
    {

        GameObject plane = GameObject.FindGameObjectWithTag("Plane");
        Vector3 newPositionForCamera =  plane.transform.position;
        newPositionForCamera.z -= 4.0f;
        newPositionForCamera.y = Camera.main.transform.position.y;
        newPositionForCamera.x = Camera.main.transform.position.x;
        plane.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        Camera.main.transform.position = newPositionForCamera;
        int screenShotNumber = PlayerPrefs.GetInt("ScreenShotNumber",0);
        string filePath = "Assets/Screenshot/screenshot" + screenShotNumber + ".png";
        Application.CaptureScreenshot(filePath);
        screenShotNumber++;
        PlayerPrefs.SetInt("ScreenShotNumber",screenShotNumber);
    }

    void OnCollisionEnter(Collision collision)
    {
        state = "stuck";
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.transform.SetParent(collision.collider.gameObject.transform);
    }
}
