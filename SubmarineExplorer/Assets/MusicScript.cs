﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

    private GameObject submarine; 
	// Use this for initialization
	void Start ()
    {
        

        submarine = GameObject.FindGameObjectWithTag("Submarine");
        AkSoundEngine.PostEvent("MainMusic", gameObject);
	}

    // Update is called once per frame
    void Update() {

        AkSoundEngine.SetRTPCValue("Depth", submarine.transform.position.y);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GlobalFishBox>())
        {
            string fish = other.GetComponent<GlobalFishBox>().fishProps.Type;
            StartCoroutine("RaiseVolume", fish);
        }
        

       
           
            

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<GlobalFishBox>())
        {
            string fish = other.GetComponent<GlobalFishBox>().fishProps.Type;
            StartCoroutine("LowerVolume", fish);
        }
    }

    public IEnumerator RaiseVolume(string fish)
    {

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.05f);
            AkSoundEngine.SetRTPCValue(fish, i);
        }
        
    }
    public IEnumerator LowerVolume(string fish)
    {

        for (int i = 100; i > 0; i--)
        {
            yield return new WaitForSeconds(0.05f);
            AkSoundEngine.SetRTPCValue(fish, i);
        }

    }
}
