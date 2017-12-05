using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard_SubmarineController : MonoBehaviour {


    public float speed = 10.0f;
    public bool inVehicle = false; 

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (inVehicle)
        {
            float translation = Input.GetAxis("Vertical") * speed;
            float strafe = Input.GetAxis("Horizontal") * speed;
            translation *= Time.deltaTime;
           strafe *= 0.5f;
            float lift = 0;

            if (Input.GetKey("space"))
            {
                lift += 0.03f;
            }
            else if (Input.GetKey("c"))
            {
                lift -= 0.03f;
            }
            else if (!Input.GetKey("space") && lift > 0)
            {
                lift -= 0.03f; 
            }
            else if (!Input.GetKey("c") && lift < 0)
            {
                lift += 0.03f;
            }



            if (lift > 7)
            {
                lift = 4;
            }
            else if (lift < -7)
            {
                lift = -4; 
            }
            transform.Rotate(Vector3.up * strafe);
            transform.Translate(0, lift, translation*-1);
        }
      

    }
}
