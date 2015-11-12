using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
      
        int screenshotNumber = PlayerPrefs.GetInt("ScreenShotNumber", 0);
        if(screenshotNumber > 0)
        screenshotNumber -= 1;
        string screenShotName = "screenshot" + screenshotNumber;
        Texture2D texture = Resources.Load(screenShotName) as Texture2D;
        if(texture)
        GetComponent<Renderer>().material.mainTexture = texture;

    }
	
	// Update is called once per frame
	void Update () {


        if (Input.GetMouseButton(0))
        {
            Application.LoadLevel(1);
        }
    }
}
