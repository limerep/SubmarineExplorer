using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSourceTag : MonoBehaviour {



    void OnEnable()
    {
        var fishprops = GetComponent<GlobalFishBox>().fishProps;
        bool exists = false;

        // Loop through all quest and check if that fish already exists.
        for (int i = 0; i < Science.questList.Count; i++)
        {
            if (Science.questList[i] == fishprops.Type)
            {
                Debug.Log("Quest already exists for " + fishprops.Type);
                exists = true;
                break;
            }
        }
        if (exists) return; // Exit out of this function if a quest for it already exists.


        // Add the quest.
        Debug.Log("Adding quest for " + fishprops.Type);
        Science.questList.Add(fishprops.Type);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
