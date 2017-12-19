using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Science : GenericButton {

    public PhotoManager photoManager; 
    public Camera terminalCamera; 
    public UnityEngine.UI.Text questText; 


    List<string> photographedCreatures;
    List<Quest> finishedQuest;
    Quest currentQuest;
    List<string> questList;  

    enum  QuestState
    {
        Started,
        Ongoing, 
        Done,
        Cancelled
    };


	// Use this for initialization
	void Start ()
    {
        finishedQuest = new List<Quest>();
        CreateQuest("", "");

        questList = new List<string>();
        questList.Add("BoomerangFish");
        questList.Add("TorpedoFish");


	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public void QuestFromPool()
    {
        int chosenQuest = Random.Range(0, questList.Count - 1);
        for (int i = 0; i < finishedQuest.Count; i++)
        {
            if (questList[chosenQuest] == finishedQuest[i].name)
            {
                
            }
        }

    }

    public void CreateQuest(string name, string description)
    {
        if (currentQuest == null)
        {
            int random = Random.Range(0, questList.Count);
            currentQuest = new Quest(questList[random]);
        }
        else
        {
            for (int i = 0; i < finishedQuest.Count; i++)
            {
                if (name != finishedQuest[i].name)
                {
                    currentQuest = new Quest(name, description);
                }
            }
            
        }
        
    }

    public void TurnInQuest(int photo)
    {
        if (photoManager.photoList[photo].getName() == currentQuest.name)
        {
            finishedQuest.Add(currentQuest);
            Debug.Log("Quest done");
            List<GameObject> creatureList = photoManager.photoList[photo].getCreatures();

            for (int i = 0; i < creatureList.Count; i++)
            {
                if (creatureList[i].GetComponent<GenericCreature>().ReturnType() != currentQuest.name)
                {
                    CreateQuest(creatureList[i].GetComponent<GenericCreature>().ReturnType(), creatureList[i].GetComponent<GenericCreature>().GetDescription());
                }
            }



            photoManager.RemovePhoto(photo);
        }
    }
    public void CompareConditions()
    {

    }

    public override void ButtonPressed(GameObject character)
    {
        character.GetComponent<Keyboard_FirstPersonController>().usingCam = true;
        Camera.main.enabled = false;
        terminalCamera.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
