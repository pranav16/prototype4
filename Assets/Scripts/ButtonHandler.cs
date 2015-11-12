using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void captureImage()
    {
        Vector3 newPositionForCamera = new Vector3(0.0f,1.0f,-5.78f);
        Camera.main.transform.position = newPositionForCamera;
        int screenShotNumber = PlayerPrefs.GetInt("ScreenShotNumber", 0);
        string filePath = "Assets/Screenshot/screenshot" + screenShotNumber + ".png";
        Application.CaptureScreenshot(filePath);
        screenShotNumber++;
        PlayerPrefs.SetInt("ScreenShotNumber", screenShotNumber);
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


