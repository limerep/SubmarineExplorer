using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard_FirstPersonController : MonoBehaviour {


    public float speed = 10.0f;
    public bool inVehicle = false;
    public bool usingCam = false;
    public Camera subCam;
    public Camera terminalCamera; 
    public Camera mainCam; 
	// Use this for initialization
	void Start () {
        //Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {


        if (inVehicle)
        {

        }
        else if (usingCam)
        {

        }
        else
        {
            float translation = Input.GetAxis("Vertical") * speed;
            float strafe = Input.GetAxis("Horizontal") * speed;
            translation *= Time.deltaTime;
            strafe *= Time.deltaTime;

            transform.Translate(strafe, 0, translation);

        }
        //Basic movement
        

        //if (Input.GetKeyDown("escape"))
        //    Cursor.lockState = CursorLockMode.None;

        if (usingCam)
        {
            if (Input.GetKeyDown("x"))
            {

                mainCam.enabled = true; 
                subCam.enabled = false;
                terminalCamera.enabled = false;
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;
                subCam.GetComponent<SubmarineCamControl>().usingCam = false; 

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
                if (hit.collider.GetComponent<GenericButton>())
                {
                    //print("This is a button, press 'F' to Activate it");
                    if (Input.GetKeyDown("f") && !usingCam)
                    {
                        AkSoundEngine.PostEvent("ButtonsPress", gameObject);
                        ActivateButton(hit);
                    }
                }

            }
        }
    }


    void ActivateButton(RaycastHit hitObject)
    {
        
      hitObject.collider.GetComponent<GenericButton>().ButtonPressed(gameObject);

    }

}
