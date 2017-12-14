using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraControl : MonoBehaviour {

    private SteamVR_TrackedObject trackedObject = null;
    private SteamVR_Controller.Device device;
    GameObject Camera;

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        Camera = GameObject.Find("Camera Shutter");
    }

    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            device.TriggerHapticPulse(1000);
            Camera.GetComponent<ShutterController>().TestRun();
        }

    }
}
