using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

    // Use this for initialization
    private int timer;
    public bool isButtonClicked;
	void Start () {
        timer = 0;
        isButtonClicked = false;
	}
	
	// Update is called once per frame
	void Update () {
	 
        if(isButtonClicked == true)
        {
            timer++;
            if (timer > 30)
                Application.LoadLevel(2);
        }

	}
    public void captureImage()
    {
   
        Vector3 newPositionForCamera = new Vector3(0.0f,1.0f,-5.78f);
        Camera.main.transform.position = newPositionForCamera;
        int screenShotNumber = PlayerPrefs.GetInt("ScreenShotNumber", 0);
        string filePath = "Assets/Resources/screenshot" + screenShotNumber + ".png";
        Application.CaptureScreenshot(filePath);
        screenShotNumber++;
        PlayerPrefs.SetInt("ScreenShotNumber", screenShotNumber);
        GameObject objectPlane = GameObject.FindGameObjectWithTag("Plane");
        ButtonHandler handler = objectPlane.GetComponent<ButtonHandler>();
        handler.isButtonClicked = true;

    }

    
    public void OnCollisionEnter(Collision collision)
    {

        TouchControls control = collision.gameObject.GetComponentInParent<TouchControls>();
        if (control)
            control.handleCollisions(gameObject);
        TouchControlRegularObjects rcontrol = collision.gameObject.GetComponentInParent<TouchControlRegularObjects>();
        if (rcontrol)
        rcontrol.handleCollisions(gameObject);

    }
}


