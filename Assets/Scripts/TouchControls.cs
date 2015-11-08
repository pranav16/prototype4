using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour
{

    // Use this for initialization

    public int swipeDistance;
    private Vector3 touchStartLocation;
    private bool isFirstTouch;
    public int speed;
  
    


    void Start()
    {
        isFirstTouch = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && isFirstTouch)
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
                    isFirstTouch = false;
                }
            }
        }
        else if (!isFirstTouch)
        {
            isFirstTouch = true;

            double touchDiffrenceY = Mathf.Abs(touchStartLocation.y - Input.mousePosition.y);
            double touchDiffrenceX = Mathf.Abs(touchStartLocation.x - Input.mousePosition.x);
            if (touchDiffrenceX >= swipeDistance || touchDiffrenceY >= swipeDistance)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Vector3 touchFinalLocation = Input.mousePosition;
                touchFinalLocation.z = 10;
                touchFinalLocation = Camera.main.ScreenToWorldPoint(touchFinalLocation);
                touchFinalLocation.Normalize();
                Vector3 currentPostion = transform.position;
                currentPostion.Normalize();
                Vector3 direction = touchFinalLocation - currentPostion;
                direction.z = 1;
                int magnitudeOfForce = speed;  
                player.GetComponent<Rigidbody>().velocity = direction * magnitudeOfForce;
            }


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
        Application.CaptureScreenshot("Assets/Screenshot.png");
    }

    void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.transform.SetParent(collision.collider.gameObject.transform);
    }
}
