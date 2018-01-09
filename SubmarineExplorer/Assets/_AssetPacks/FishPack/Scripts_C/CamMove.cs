using UnityEngine;

public class CamMove : MonoBehaviour {

    public GameObject mRoot ;

    public float mRotSinAmp;
    public float mRotSinSpeed;

    private float mRotSinTime;
    private float mRotYDefault;


    public float mPosSinAmp;
    public float mPosSinSpeed;

    private float mPosSinTime;
    private float mPosYDefault;
        
   void Start ()
   {
        if (!mRoot) mRoot = gameObject;

        SetDefaults(mRoot);
    }

    void SetDefaults(GameObject aCamGO)
    {
        mRotYDefault = aCamGO.transform.rotation.eulerAngles.y;
        mPosYDefault = aCamGO.transform.position.y;

    }

    void LateUpdate()
    {
        mRotSinTime += (mRotSinSpeed * Time.deltaTime);
        float jumpy = Mathf.Sin(mRotSinTime);
        mRoot.transform.rotation = Quaternion.Euler(mRoot.transform.rotation.eulerAngles.x, mRotYDefault + (jumpy * mRotSinAmp), mRoot.transform.rotation.eulerAngles.z );

        mPosSinTime += (mPosSinSpeed * Time.deltaTime);
        jumpy = Mathf.Sin(mPosSinTime);
        mRoot.transform.position  = new Vector3(mRoot.transform.position.x, mPosYDefault + (jumpy * mPosSinAmp), mRoot.transform.position .z);
    }
}
