using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.AI;

public class Keyboard_SubmarineController : MonoBehaviour {


    public float speed = 10.0f;
    public bool inVehicle = false;
    public GameObject submarine;

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private Vector2 touchpad;


    // Navigation variables
    [SerializeField]
    private Transform navigationPlane;
    private Vector3 targetPosition;
    private Vector3 startDirection;
    private float maxSpeed = 4;
    private NavMeshAgent navAgent;

    // Use this for initialization
    void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObject.index);
        targetPosition = submarine.transform.position + submarine.transform.forward * 1000;
        startDirection = targetPosition - submarine.transform.position;
        startDirection = startDirection.normalized;
        navAgent = submarine.GetComponent<NavMeshAgent>();
        navAgent.speed = 0;
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
            translation *= 0.1f;


            navAgent.speed = Mathf.Min(navAgent.speed, maxSpeed);

            if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                navigationPlane.GetComponent<NavAreaMove>().Dive(touchpad.y);
            }
            else
            {
                if (Mathf.Abs(touchpad.y) > 0.5f)
                {
                    navAgent.speed += touchpad.y * Time.deltaTime;
                }

            }

            if (Mathf.Abs(strafe) > 0.5f)
            {
                submarine.transform.Rotate(Vector3.up * strafe * Time.deltaTime * 15, Space.Self);
            }
            //submarine.transform.Translate(0, lift, translation*-1);

            targetPosition = submarine.transform.position + submarine.transform.forward * 1000;

            Vector3 navPlanePos = submarine.transform.position;
            navPlanePos.y = navigationPlane.position.y;
            submarine.transform.position = navPlanePos;
        }

        Vector3 heading = targetPosition - submarine.transform.position;
        float distanceToTarget = heading.magnitude;

        Vector3 direction = heading / distanceToTarget;

        submarine.transform.rotation = Quaternion.Slerp(submarine.transform.rotation, Quaternion.LookRotation(direction), 0.1f * Time.deltaTime);
        navAgent.destination = submarine.transform.position + submarine.transform.forward * 15;
    }
}
