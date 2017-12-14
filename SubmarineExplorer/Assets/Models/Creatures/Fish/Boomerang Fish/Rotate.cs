using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float speedX = 5f;
    public float speedY = 5f;
    public float speedZ = 5f; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(speedX, speedY, speedZ);
	}
}
