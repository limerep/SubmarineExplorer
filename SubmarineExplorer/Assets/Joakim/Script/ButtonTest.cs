using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : GenericButton {

    public Camera subCam; 

    public override void ButtonPressed(GameObject character)
    {
        character.GetComponent<Keyboard_FirstPersonController>().usingCam = true; 
        Camera.main.enabled = false; 
        subCam.enabled = true;  
    }
}
