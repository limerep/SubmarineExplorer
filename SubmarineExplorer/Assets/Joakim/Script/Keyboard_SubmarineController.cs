using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Keyboard_SubmarineController : MonoBehaviour {


    public float speed = 10.0f;
    public bool inVehicle = false;
    public GameObject submarine;

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private Vector2 touchpad; 
   

    // Use this for initialization
    void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObject.index);
    }
	
	// Update is called once per frame
	void Update () {


        if (inVehicle)
        {

            //float translation = Input.GetAxis("Vertical") * speed;
            //float strafe = Input.GetAxis("Horizontal") * speed;

            float translation;
            float strafe;

            
            touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            translation = touchpad.y;
            strafe = touchpad.x;
            strafe *= 0.5f;
            translation *= 0.1f;
            float lift = 0;

            if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                lift = touchpad.y;
                lift *= 0.1f; 
                translation = 0; 

            }
            else
            {
                lift = 0; 
            }
            
           
            
            
            

            //if (Input.GetKey("space"))
            //{
            //    lift += 0.03f;
            //}
            //else if (Input.GetKey("c"))
            //{
            //    lift -= 0.03f;
            //}
            //else if (!Input.GetKey("space") && lift > 0)
            //{
            //    lift -= 0.03f; 
            //}
            //else if (!Input.GetKey("c") && lift < 0)
            //{
            //    lift += 0.03f;
            //}

            //if (lift > 7)
            //{
            //    lift = 4;
            //}
            //else if (lift < -7)
            //{
            //    lift = -4; 
            //}
            submarine.transform.Rotate(Vector3.up * strafe);
            submarine.transform.Translate(0, lift, translation*-1);
        }
      

    }
}
