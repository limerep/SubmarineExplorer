using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV_CS : MonoBehaviour {

    public float scrollSpeed_X = 0.5f;
    public float scrollSpeed_Y = 0.5f;
    public int materialIndex = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float offsetX = Time.time * scrollSpeed_X;
        float offsetY = Time.time * scrollSpeed_Y;
        GetComponent<Renderer>().materials[materialIndex].mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
