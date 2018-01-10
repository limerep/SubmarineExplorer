using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineCreaking : MonoBehaviour {

	// Use this for initialization
	void Start () {
        AkSoundEngine.PostEvent("Submarine_Creaking", gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
