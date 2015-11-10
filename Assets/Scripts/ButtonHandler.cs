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

  
        Vector3 newPositionForCamera = transform.position;
        newPositionForCamera.z -= 4.0f;
        newPositionForCamera.y = Camera.main.transform.position.y;
        newPositionForCamera.x = Camera.main.transform.position.x;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Camera.main.transform.position = newPositionForCamera;
        int screenShotNumber = PlayerPrefs.GetInt("ScreenShotNumber", 0);
        string filePath = "Assets/Screenshot/screenshot" + screenShotNumber + ".png";
        Application.CaptureScreenshot(filePath);
        screenShotNumber++;
        PlayerPrefs.SetInt("ScreenShotNumber", screenShotNumber);
    }
}
