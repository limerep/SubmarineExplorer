﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        AkSoundEngine.PostEvent("StartMenuMusic", gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}