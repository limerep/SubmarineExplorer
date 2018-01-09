using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabChange : MonoBehaviour {

	public List<GameObject> mPrefabs = new List<GameObject>();

    private int mIndex = 0;
    private Transform  mRefPos;
    private GameObject mActual;

    public GUIText mText ;

    void Start () {
        mRefPos = gameObject.transform;
        Load();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mIndex++;
            mIndex = Mathf.Abs(mIndex % mPrefabs.Count);
            Load();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mIndex--;
            mIndex = Mathf.Abs(mIndex % mPrefabs.Count);
            Load();
        }
    }


    void Load()
    {
        if (mActual)
            DestroyImmediate(mActual);

        if (mPrefabs.Count > 0)
        {
            mActual = Instantiate(mPrefabs[mIndex]);
            mActual.transform.position = mRefPos.position;
            mActual.transform.rotation = mRefPos.rotation;

            if (mText)
            {
                mText.text = mIndex + " " + mPrefabs[mIndex].name;
            }
        }
    }
}
