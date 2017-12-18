using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRControllers : MonoBehaviour
{

    public SteamVR_TrackedObject mTrackedObject = null;
    public SteamVR_Controller.Device mDevice;

	void Awake ()
    {
        mTrackedObject = GetComponent<SteamVR_TrackedObject>(); // Get trackedObject from controller
	}
	

	void Update ()
    {
        mDevice = SteamVR_Controller.Input((int)mTrackedObject.index);

        #region Trigger

        // Down
        if (mDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            print("Trigger Down");
        }

        // Up
        if (mDevice.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            print("Trigger Up");
        }

        // Value
        Vector2 triggerValue = mDevice.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger);
        #endregion

        #region Grip
        //Down 
        if(mDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            print("Grip Down");
        }

        //Up 
        if (mDevice.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            print("Grip Up");
        }

        #endregion

        #region Touchpad
        //Down 
        if (mDevice.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            print("Touchpad Down");
        }

        if(mDevice.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            print("Touchpad Up");
        }

        //Value
        Vector2 touchValue = mDevice.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
        print(touchValue);

        #endregion
    }
}
