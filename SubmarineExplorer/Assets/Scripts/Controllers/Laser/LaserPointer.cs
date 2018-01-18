using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LaserPointer : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;
    public bool inCam = false;
    public bool inVehicle = false;
    private bool canVibrate = true;
    private bool hasVibrated = false;

    // LASER //
    // 1 This is a reference to the Laser’s prefab.
    public GameObject laserPrefab;
    // 2  stores a reference to an instance of the laser.
    private GameObject laser;
    // 3 The transform component is stored for ease of use.
    private Transform laserTransform;
    // 4 This is the position where the laser hits.
    private Vector3 hitPoint;

    // RETICLE //
    // 1 Is the transform of [CameraRig].
    public Transform cameraRigTransform;
    // 2 Stores a reference to the teleport reticle prefab.
    public GameObject teleportReticlePrefab;
    // 3 A reference to an instance of the reticle.
    private GameObject reticle;
    // 4 Stores a reference to the teleport reticle transform for ease of use.
    private Transform teleportReticleTransform;
    // 5 Stores a reference to the player’s head (the camera).
    public Transform headTransform;
    // 6 Is the reticle offset from the floor, so there’s no “Z-fighting” with other surfaces.
    public Vector3 teleportReticleOffset;
    // 7 Is a layer mask to filter the areas on which teleports are allowed.
    public LayerMask teleportMask;
    // 8 Is set to true when a valid teleport location is found.
    private bool shouldTeleport;


    private void Start()
    {
        // 1 spawn a new laser and save a reference to it in laser.
        laser = Instantiate(laserPrefab);
        // 2 Store the laser’s transform component.
        laserTransform = laser.transform;

        // 1 Spawn a new reticle and save a reference to it in reticle.
        reticle = Instantiate(teleportReticlePrefab);
        // 2 Store the reticle’s transform component.
        teleportReticleTransform = reticle.transform;

    }
    private void Update()
    {
        Scene mainScene = SceneManager.GetActiveScene();
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (mainScene.name == "MainMenu")
        {
            //if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
            //{
                RaycastHit hit;
    
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100))
            {
                hitPoint = hit.point;
                ShowLaser(hit);
                canVibrate = true;

                if(canVibrate && !hasVibrated)
                {
                    Debug.Log("VIBRERA FÖR FAN");
                    StartCoroutine(LongVibration(0.05f, 550));
                }
             
                Button button = hit.collider.GetComponent<Button>();
                if (hit.collider.tag == "Button")
                {
                    button.animator.Play("Button_Highlight");
                    if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                        button.animator.Play("Button_Pressed");
                    }
                }
            }
            else
            { 
                laser.SetActive(false);
                canVibrate = false;
                hasVibrated = false;
            }

        } else
        {
            if (!inCam && !inVehicle)
            {
                // 1 If the touchpad is held down
                if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    RaycastHit hit;

                    // 2 Shoot a ray from the controller. If it hits something, make it store the point where it hit and show the laser.
                    if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
                    {
                        hitPoint = hit.point;
                        ShowLaser(hit);

                        // 1 Show the teleport reticle
                        reticle.SetActive(true);
                        // 2 Move the reticle to where the raycast hit with the addition of an offset to avoid Z-fighting.
                        teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                        // 3 Set shouldTeleport to true to indicate the script found a valid position for teleporting.
                        shouldTeleport = true;
                    }
                }
                else // 3 Hide the laser when the player released the touchpad.
                {
                    laser.SetActive(false);
                    reticle.SetActive(false);
                }

                if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport)
                {
                    Teleport();
                }
            }
        } 
    }

    // Teleport player
    private void Teleport()
    {
        // 1 Set the shouldTeleport flag to false when teleportation is in progress.
        shouldTeleport = false;
        // 2 Hide the reticle.
        reticle.SetActive(false);
        // 3 Calculate the difference between the positions of the camera rig’s center and the player’s head.
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        // 4 Reset the y-position for the above difference to 0, because the calculation doesn’t consider the vertical position of the player’s head.
        difference.y = 0;
        // 5 Move the camera rig to the position of the hit point and add the calculated difference. Without the difference, the player would teleport to an incorrect location. 
        cameraRigTransform.position = hitPoint + difference;
    }

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void ShowLaser(RaycastHit hit)
    {
        // 1 Show the laser.
        laser.SetActive(true);

        // 2 Position the laser between the controller and the point where the raycast hits. 
        //Lerp because you can give it two positions and the percent it should travel. If you pass it 0.5f, which is 50%, it returns the precise middle point.
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, .5f);

        // 3 Point the laser at the position where the raycast hit.
        laserTransform.LookAt(hitPoint);

        // 4 Scale the laser so it fits perfectly between the two positions.
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
    }

    IEnumerator LongVibration(float length, ushort strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            device.TriggerHapticPulse(strength);
            yield return null; //every single frame for the duration of "length" you will vibrate at "strength" amount
            hasVibrated = true;
        }
    }
}
