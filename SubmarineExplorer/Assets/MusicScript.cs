using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {

    private GameObject submarine; 
	// Use this for initialization
	void Start ()
    {
        //AkSoundEngine.PostEvent("MenuMusic", gameObject);

        submarine = GameObject.FindGameObjectWithTag("Submarine");
        AkSoundEngine.PostEvent("MainMusic", gameObject);
	}

    // Update is called once per frame
    void Update() {

        if (submarine.transform.position.y >= 0)
        {
            AkSoundEngine.SetSwitch("Interactive_Music", "Layer_0", gameObject);
        }
        else if (submarine.transform.position.y < 0 && submarine.transform.position.y > -20)
        {
            AkSoundEngine.SetSwitch("Interactive_Music", "Layer1", gameObject);
        }
        else if (submarine.transform.position.y < -20 && submarine.transform.position.y > -50)
        {
            AkSoundEngine.SetSwitch("Interactive_Music", "Layer2", gameObject);
        }

    }
}
