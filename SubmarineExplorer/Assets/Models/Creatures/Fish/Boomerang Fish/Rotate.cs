using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float speedX = 5f;
    public float speedY = 5f;
    public float speedZ = 5f; 
	
	void Update ()
    {
        transform.Rotate(new Vector3(speedX, speedY, speedZ) * Time.deltaTime);
	}
}
