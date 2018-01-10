using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAndTest : MonoBehaviour {
	public GameObject testObject;
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown("up"))
		testObject.GetComponent<ShutterController>().RunShutter();
	}
}
