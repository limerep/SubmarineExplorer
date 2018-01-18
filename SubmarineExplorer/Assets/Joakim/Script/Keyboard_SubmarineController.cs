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
    private Vector3 targetDirection;
    private float maxSpeed = 4;
    private NavMeshAgent navAgent;

    // Use this for initialization
    void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObject.index);
        targetPosition = submarine.transform.position + submarine.transform.forward * 1000;
        targetDirection = targetPosition - submarine.transform.position;
        targetDirection = targetDirection.normalized;
        navAgent = submarine.GetComponent<NavMeshAgent>();
        navAgent.speed = 0;
    }
	
	// Update is called once per frame
	void Update () {


        if (inVehicle)
        {

            //float translation = Input.GetAxis("Vertical") * speed;
            //float strafe = Input.GetAxis("Horizontal") * speed;
            
            float strafe = 0.0f;
            float accelerate = 0.0f;
            
            touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            
            strafe = touchpad.x;
            accelerate = touchpad.y;
            
            if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                navigationPlane.GetComponent<NavAreaMove>().Dive(touchpad.y);
            }
            else
            {
                if (Mathf.Abs(accelerate) > 0.5f && device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    navAgent.speed += Mathf.Round(accelerate);
                    navAgent.speed = Mathf.Min(navAgent.speed, maxSpeed);
                }

            }

            if (Mathf.Abs(strafe) > 0.5f)
            {
                submarine.transform.Rotate(Vector3.up * strafe * Time.deltaTime * 15, Space.Self);
            }

            // Always update targetdirection and position while we're in pilot mode.
            targetPosition = submarine.transform.position + submarine.transform.forward * 1000;
            targetDirection = targetPosition - submarine.transform.position;
            targetDirection = targetDirection.normalized;
        }

        Vector3 heading = targetPosition - submarine.transform.position;
        float distanceToTarget = heading.magnitude;

        Vector3 direction = heading / distanceToTarget;

        submarine.transform.rotation = Quaternion.Slerp(submarine.transform.rotation, Quaternion.LookRotation(direction), 0.1f * Time.deltaTime);
        navAgent.destination = submarine.transform.position + submarine.transform.forward * 15;

        Vector3 navPlanePos = submarine.transform.position;
        navPlanePos.y = navigationPlane.position.y;
        submarine.transform.position = navPlanePos;


        if (navAgent.speed == 0)
        {
            Debug.Log(navAgent.velocity.magnitude);
            navAgent.destination = submarine.transform.position + submarine.transform.forward * 15 * navAgent.velocity.magnitude * 0.5f;
            navAgent.isStopped = true;

            if (navAgent.velocity.magnitude < 0.6f)
            {
                navAgent.velocity = Vector3.zero;
            }
        }
        else
        {
            navAgent.isStopped = false;
        }

        // Calculate new target position if we're getting close to our goal
        if (distanceToTarget < 100)
        {
            targetPosition = submarine.transform.position + targetDirection * 1000;
        }
    }
}
