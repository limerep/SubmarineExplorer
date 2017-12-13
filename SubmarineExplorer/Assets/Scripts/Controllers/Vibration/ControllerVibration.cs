using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerVibration : MonoBehaviour {


    public bool hapticFlag = false;
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    void Start ()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
	}
	
	
	void Update ()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);
        if(hapticFlag)
        {
            device.TriggerHapticPulse(1500);
        }
	}

    void OnTriggerEnter (Collider other)
    {
        hapticFlag = true;
    }

    private void OnTriggerExit (Collider other)
    {
        hapticFlag = false;
    }
}


//    If you want longer vibrations, try something like this:

////length is how long the vibration should go for
////strength is vibration strength from 0-1
//    IEnumerator LongVibration(float length, float strength)
//    {
//        for (float i = 0; i < length; i += Time.deltaTime)
//        {
//            SteamVR_Controller.Input([index]).TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
//        yield return null;
//    }
//}

//And for pulsed vibrations
////vibrationCount is how many vibrations
////vibrationLength is how long each vibration should go for
////gapLength is how long to wait between vibrations
////strength is vibration strength from 0-1
//IEnumerator LongVibration(int vibrationCount, float vibrationLength, float gapLength, float strength)
//{
//    strength = Mathf.Clamp01(strength);
//    for (int i = 0; i < vibrationCount; i++)
//    {
//        if (i != 0) yield return new WaitForSeconds(gapLength);
//        yield return StartCoroutine(LongVibration(vibrationLength, strength));
//    }
//}


