using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineButtonScript : GenericButton {

    public GameObject submarine; 

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
}