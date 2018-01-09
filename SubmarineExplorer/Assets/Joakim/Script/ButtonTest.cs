using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : GenericButton {

    public Camera subCam;
    public GameObject cameraPosition; 

    public override void ButtonPressed(GameObject character)
    {
        character.GetComponent<Keyboard_FirstPersonController>().usingCam = true; 
        Camera.main.enabled = false; 
        subCam.enabled = true;
        subCam.GetComponent<SubmarineCamControl>().usingCam = true; 
    }


    public override void VrButtonPress(GameObject character)
    {
        
        character.transform.parent.gameObject.transform.position = cameraPosition.transform.position;
        character.GetComponentInChildren<LaserPointer>().inCam = true;
        character.GetComponentInChildren<VRButtonControls>().inCam = true;
    }
}
