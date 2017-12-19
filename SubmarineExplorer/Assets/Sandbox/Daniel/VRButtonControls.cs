using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Cursor.lockState = CursorLockMode.Locked;
        loadingCircle.transform.position = vrCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 15));

    }
	
	// Update is called once per frame
	void Update () {

        if (inVehicle)
        {
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                gameObject.transform.parent.gameObject.transform.parent = null;
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
                gameObject.transform.parent.transform.position = originalPos.transform.position;
                inCam = false;
                gameObject.GetComponent<LaserPointer>().inCam = false;
                loadingCircle.GetComponent<SpriteRenderer>().enabled = false;
                cameraCanvas.SetActive(false);

            }


            if (Physics.Raycast(ray, out hit, 50, 5))
            {
                
                Debug.DrawLine(vrCamera.transform.position, hit.point, Color.red);

              

                if (hit.collider.GetComponent<GenericCreature>())
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
                        
                        cameraCanvas.SetActive(false);
                        StartCoroutine("TextureScreenshot", hit);
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
                other.GetComponent<GenericButton>().VrButtonPress(gameObject);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        print("Hej");
    }

    private void OnTriggerExit(Collider other)
    {
        print("Hej");

    }

    IEnumerator TextureScreenshot(RaycastHit hit)
    {
        //loadingCircle.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenShot.Apply();

        planes = GeometryUtility.CalculateFrustumPlanes(vrCamera);

        Collider[] colliders = Physics.OverlapBox(hit.point, new Vector3(200, 200, 200));

        List<string> creatures = new List<string>();

        for (int i = 0; i < colliders.Length; i++)
        {
            if (GeometryUtility.TestPlanesAABB(planes, colliders[i].bounds) && colliders[i].gameObject.GetComponent<GenericCreature>())
            {
                creatures.Add(colliders[i].gameObject.GetComponent<GenericCreature>().ReturnType());
            }
        }

        photoTest.GetComponent<Renderer>().material.EnableKeyword("_MainTex");

        photoTest.GetComponent<Renderer>().material.SetTexture("_MainTex", screenShot);

        string fish = hit.collider.GetComponent<GenericCreature>().ReturnType();

        photoManager.GetComponent<PhotoManager>().CreatePhoto(fish, screenShot, creatures);
        yield return new WaitForSeconds(0.1f);
        //loadingCircle.GetComponent<SpriteRenderer>().enabled = true;
        cameraShutter.GetComponent<ShutterController>().RunShutter();
        cameraCanvas.SetActive(true);

    }
}
