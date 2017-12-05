using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SubmarineCamControl : MonoBehaviour {

    public GameObject loadingCircle;
    public GameObject photoManager;
    public GameObject photoTest;
    public WWW w;
    bool playingAnimation; 
    
  
    Animator loadingAnimation;
    public Camera subCamera; 
    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;


    Quaternion originalRotation; 
    // Use this for initialization
    void Start () {

        originalRotation = transform.rotation;
        loadingAnimation = loadingCircle.GetComponent<Animator>();
        loadingAnimation.speed = 0; 
      
        
    }
	
	// Update is called once per frame
	void Update () {


        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1 / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1 / smoothing);
        mouseLook += smoothV;
        mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);


        Quaternion xQuaternion = Quaternion.AngleAxis(mouseLook.x, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);

        transform.localRotation = originalRotation * xQuaternion * yQuaternion;

      
        
        Ray ray = subCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50))
        {

            Debug.DrawLine(this.transform.position, hit.point, Color.red);

            if (hit.collider.GetComponent<GenericCreature>())
            {

                //Control animation speed of the loading circle
                loadingAnimation.speed += 0.01f;
                if (loadingAnimation.speed > 1)
                {
                    loadingAnimation.speed = 1;
                }


                //Capture Image
                if (Input.GetMouseButtonDown(0))
                {
                    TextureScreenshot(); 
                    //TakePhoto(hit);
                    //SetTexture(hit);
                }

            }
            else
            {
                //Animation circle; 
                loadingAnimation.speed -= 0.01f; 
                if (loadingAnimation.speed < 0)
                {
                    loadingAnimation.speed = 0;
                }
                    
                
            }

        }
        else
        {
            //Control animation speed
            loadingAnimation.speed -= 0.01f;
            if (loadingAnimation.speed < 0)
            {
                loadingAnimation.speed = 0;
            }
        }
          
     
    }

    void TakePhoto(RaycastHit hit)
    {
        int photos = photoManager.GetComponent<PhotoManager>().photos;

        ScreenCapture.CaptureScreenshot("D:\\Screenshots\\Photo" + photos + ".png");    
    }

    void SetTexture(RaycastHit hit)
    {
        int photos = photoManager.GetComponent<PhotoManager>().photos;

        w = new WWW("file://D://Screenshots//Photo" + photos + ".png");


                Texture2D tex = new Texture2D(250, 250);

        w.LoadImageIntoTexture(tex);


        photoTest.GetComponent<Renderer>().material.EnableKeyword("_MainTex");

        photoTest.GetComponent<Renderer>().material.SetTexture("_MainTex", tex);


        photoManager.GetComponent<PhotoManager>().photos++;

        photoManager.GetComponent<PhotoManager>().CreatePhoto("D:\\Screenshots\\Photo" + photos + ".png", hit.collider.gameObject);
 
    }

    void TextureScreenshot()
    {
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0,0);
        screenShot.Apply();

        photoTest.GetComponent<Renderer>().material.EnableKeyword("_MainTex");

        photoTest.GetComponent<Renderer>().material.SetTexture("_MainTex", screenShot);


        //photoManager.GetComponent<PhotoManager>().photos++;

        //photoManager.GetComponent<PhotoManager>().CreatePhoto("D:\\Screenshots\\Photo" + photos + ".png", hit.collider.gameObject);

    }
  
}
