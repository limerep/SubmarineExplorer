using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard_FirstPersonController : MonoBehaviour {


    public float speed = 10.0f;
    public bool inVehicle = false;
    public bool usingCam = false;
    public Camera subCam;
    public Camera mainCam; 
	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {


        if (inVehicle)
        {

        }
        else if (usingCam)
        { }
        else
        {
            float translation = Input.GetAxis("Vertical") * speed;
            float strafe = Input.GetAxis("Horizontal") * speed;
            translation *= Time.deltaTime;
            strafe *= Time.deltaTime;

            transform.Translate(strafe, 0, translation);

        }
        //Basic movement
        

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        if (usingCam)
        {
            if (Input.GetKeyDown("x"))
            {

                mainCam.enabled = true; 
                subCam.enabled = false;
                usingCam = false; 
            }
        }

		
	}

    void FixedUpdate()
    {
       

        if (!usingCam)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1))
            {
                if (hit.collider.tag.Equals("Button"))
                {
                    //print("This is a button, press 'F' to Activate it");
                    if (Input.GetKeyDown("f") && !usingCam)
                    {
                        ActivateButton(hit);
                    }
                }

            }
        }
    }


    void ActivateButton(RaycastHit hitObject)
    {

        if (hitObject.collider.GetComponent<GenericButton>())
        {
            hitObject.collider.GetComponent<GenericButton>().ButtonPressed(gameObject);
        }

        ////Button for driving the submarine 
        //if (hitObject.collider.GetComponent<SubmarineButtonScript>())
        //{
        //    hitObject.collider.GetComponent<SubmarineButtonScript>().ButtonPressed(gameObject);

        //}
        ////Button for testing
        //else if (hitObject.collider.GetComponent<ButtonTest>())
        //{
        //    hitObject.collider.GetComponent<ButtonTest>().ButtonPressed(gameObject);
        //}
    }

}
