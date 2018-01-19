using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRButtonControls : MonoBehaviour {


   
    public bool inCam = false;
    public bool inVehicle = false; 

    public GameObject cameraPosition;
    public GameObject loadingCircle;
    public GameObject cameraCanvas;
    public GameObject cameraShutter;
    public GameObject photoManager;
    public GameObject photoTest;
    public Camera vrCamera;
    public GameObject originalPos;
    public GameObject submarine; 
    private Plane[] planes;
    bool playingAnimation;
    bool coroutineIsRunning;
    Animator loadingAnimation;

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;

    // Use this for initialization
    void Start () {

        trackedObject = GetComponent<SteamVR_TrackedObject>();
        device = SteamVR_Controller.Input((int)trackedObject.index);
        loadingCircle.GetComponent<SpriteRenderer>().enabled = false;
        cameraCanvas.SetActive(false);
        loadingAnimation = loadingCircle.GetComponent<Animator>();
        loadingAnimation.speed = 0;
        //Cursor.lockState = CursorLockMode.Locked;
        loadingCircle.transform.position = vrCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 15));

    }
	
	// Update is called once per frame
	void Update () {

        if (inVehicle)
        {

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                StartCoroutine(LongVibration(0.1f, 2000));
                //gameObject.transform.parent.gameObject.transform.parent = null;
                gameObject.GetComponent<LaserPointer>().inVehicle = false;
                inVehicle = false;
                gameObject.GetComponent<Keyboard_SubmarineController>().inVehicle = false;
            }
            //gameObject.transform.parent.transform.position = originalPos.transform.position;
        }

        if (inCam)
        {
            loadingCircle.GetComponent<SpriteRenderer>().enabled = true;
            cameraCanvas.SetActive(true);
            Ray ray = vrCamera.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0f));
            RaycastHit hit;

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                StartCoroutine(LongVibration(0.1f, 2000));
                gameObject.transform.parent.transform.position = originalPos.transform.position;
                inCam = false;
                gameObject.GetComponent<LaserPointer>().inCam = false;
                loadingCircle.GetComponent<SpriteRenderer>().enabled = false;
                cameraCanvas.SetActive(false);

            }


            if (Physics.Raycast(ray, out hit, 50, 5))
            {
                
                Debug.DrawLine(vrCamera.transform.position, hit.point, Color.red);

              

                if (hit.collider.GetComponent<GlobalFishBox>())
                {

                    //Control animation speed of the loading circle
                    loadingAnimation.speed += 0.01f;
                    if (loadingAnimation.speed > 1)
                    {
                        loadingAnimation.speed = 1;
                    }


                    //Capture Image
                    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                    {
                        

                        if (!coroutineIsRunning)
                        {
                            StartCoroutine(LongVibration(0.2f, 2000));
                            
                            StartCoroutine("TextureScreenshot", hit);
                        }
                        
                    }

                }
                else
                {
                    //Animation circle; 
                    loadingAnimation.speed -= 0.01f;
                    if (loadingAnimation.speed <= 0)
                    {
                        loadingAnimation.speed = 0;
                    }
                }
            }
            else
            {
                //Control animation speed
                loadingAnimation.speed -= 0.01f;
                if (loadingAnimation.speed <= 0)
                {
                    loadingAnimation.speed = 0;
                }
            }
        }

    }

    
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<GenericButton>())
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                StartCoroutine(LongVibration(0.1f, 2000));
                other.GetComponent<GenericButton>().VrButtonPress(gameObject);
                Debug.Log("Hit");
            }
        }

        if (other.tag == "Button")
        {
            
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {

                other.GetComponent<Button>().onClick.Invoke();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GenericButton>())
            print("Trigger enter");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<GenericButton>())
            print("Trigger exit");

    }

    IEnumerator TextureScreenshot(RaycastHit hit)
    {

        coroutineIsRunning = true;
        cameraCanvas.SetActive(false);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();

        planes = GeometryUtility.CalculateFrustumPlanes(vrCamera);

        Collider[] colliders = Physics.OverlapBox(hit.point, new Vector3(200, 200, 200));

        List<GameObject> creatures = new List<GameObject>();

        for (int i = 0; i < colliders.Length; i++)
        {
            if (GeometryUtility.TestPlanesAABB(planes, colliders[i].bounds) && colliders[i].gameObject.GetComponent<GlobalFishBox>())
            {
                creatures.Add(colliders[i].gameObject);
            }
        }

        //photoTest.GetComponent<Renderer>().material.EnableKeyword("_MainTex");

        //photoTest.GetComponent<Renderer>().material.SetTexture("_MainTex", screenShot);

        string fish = hit.collider.GetComponent<GlobalFishBox>().fishProps.Type;

        photoManager.GetComponent<PhotoManager>().CreatePhoto(fish, screenShot, creatures);

        cameraCanvas.SetActive(true);
        cameraShutter.GetComponent<ShutterController>().RunShutter();
        yield return new WaitForSeconds(1f);
        coroutineIsRunning = false; 
    }

    IEnumerator LongVibration(float length, ushort strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            device.TriggerHapticPulse(strength);
            yield return null; //every single frame for the duration of "length" you will vibrate at "strength" amount
        }
    }
}
