﻿using UnityEngine;
using System.Collections;

public class TouchControls : MonoBehaviour
{

    // Use this for initialization

    public int swipeDistance;
    private Vector3 touchStartLocation;
    private bool isFirstTouch;
    public int speed;
    private Vector3 intialPosition;
    private Vector3 directionOfmovement;

    void Start()
    {
        isFirstTouch = true;
        intialPosition = transform.position;

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
                Vector3 normalizeCameraPosition = transform.position;
                normalizeCameraPosition.Normalize();
                Vector3 direction = touchFinalLocation - normalizeCameraPosition;
                int magnitudeOfForce = speed;// + (int)touchDiffrenceX + (int)touchDiffrenceY;
                directionOfmovement = direction;
                // player.GetComponent<Rigidbody>().AddForce(direction * magnitudeOfForce);
                player.GetComponent<Rigidbody>().velocity = direction * magnitudeOfForce;
            }


        }
    
        if(transform.position.z > 10 )
        {

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Rigidbody>().velocity = 0 * directionOfmovement;
            transform.position = intialPosition;

        }


    }

    void OnCollisionEnter(Collision collision)
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody>().velocity = 0 * directionOfmovement;
        transform.position = intialPosition;


    }
}