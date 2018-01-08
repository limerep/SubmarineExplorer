using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAreaMove : MonoBehaviour {

    [SerializeField]
    private Transform submarine;
    [SerializeField]
    private float diveSpeed = 2.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        
        pos.x = submarine.position.x;
        pos.z = submarine.position.z;

        transform.position = pos;
    }

    public void Dive(float amount)
    {
        Vector3 pos = transform.position;

        pos.y += amount * diveSpeed * Time.deltaTime;
        transform.position = pos;
    }
}
