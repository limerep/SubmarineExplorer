using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineButtonScript : GenericButton {

    public GameObject submarine;
    public GameObject cameraPosition; 


    public override void ButtonPressed(GameObject character)
    {
        if (character.GetComponent<Keyboard_FirstPersonController>().inVehicle == false)
        {
            character.GetComponent<Keyboard_FirstPersonController>().inVehicle = true;
            character.transform.parent = submarine.transform;
            submarine.GetComponent<Keyboard_SubmarineController>().inVehicle = true; 
            print("Hej");
        }
        else if (character.GetComponent<Keyboard_FirstPersonController>().inVehicle == true)
        {
            submarine.GetComponent<Keyboard_SubmarineController>().inVehicle = false;
            character.GetComponent<Keyboard_FirstPersonController>().inVehicle = false;
            character.transform.parent = null; 
        }
    }


    public override void VrButtonPress(GameObject character)
    {
        if (character.GetComponent<VRButtonControls>().inVehicle == false)
        {
            character.GetComponent<VRButtonControls>().inVehicle = true;
            character.GetComponent<LaserPointer>().inVehicle = true;
            character.GetComponent<Keyboard_SubmarineController>().inVehicle = true;
            character.transform.parent.gameObject.transform.parent = submarine.transform; 
        }
        else if (character.GetComponent<VRButtonControls>().inVehicle == true)
        {
            
            character.GetComponent<LaserPointer>().inVehicle = false;
            character.GetComponent<VRButtonControls>().inVehicle = false;
            character.GetComponent<Keyboard_SubmarineController>().inVehicle = false;
            character.transform.parent.gameObject.transform.parent = null; 


        }
    }
}