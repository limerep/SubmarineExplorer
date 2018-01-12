using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleSounds : MonoBehaviour {

    GameObject submarine;
    bool checkTimer;
    public bool playerClose;
    public int time;
    public float clockTime; 

    // Use this for initialization
    void Start () {
        time = Random.Range(3, 10);
        clockTime = 0; 
	}
	
	// Update is called once per frame
	void Update ()
    {
       

        if (playerClose)
        {
            if (clockTime > time)
            {
                StartCoroutine("PlayWhaleSounds");
                time = Random.Range(3, 10);
                clockTime = 0;
            }
            else
            {
                clockTime += Time.deltaTime; 
            }
            
        }


	}

    IEnumerator PlayWhaleSounds()
    {
        AkSoundEngine.PostEvent("WhaleSong", gameObject);

        yield return null; 
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Hej");
        if (other.tag == "Submarine")
        {
            playerClose = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Submarine")
        {
            playerClose = false;
        }
    }
}
