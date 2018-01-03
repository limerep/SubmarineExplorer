using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavAreaMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 oldPos = transform.position;
            oldPos.y += Time.deltaTime;
            transform.position = oldPos;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 oldPos = transform.position;
            oldPos.y -= Time.deltaTime;
            transform.position = oldPos;
        }
    }
}
