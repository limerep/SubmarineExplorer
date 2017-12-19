using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Science : GenericButton {

    public PhotoManager photoManager; 
    public Camera terminalCamera; 
    public UnityEngine.UI.Text questText; 


    List<string> photographedCreatures;
    public List<Quest> finishedQuest;
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
        

        questList = new List<string>();
        questList.Add("BoomerangFish");
        questList.Add("BulletFish");
        

        CreateQuest("", "");


    }
	
	// Update is called once per frame
	void Update ()
    {
        questText.text = currentQuest.name;
	}


    public void QuestFromPool()
    {
        print("Quest From Pool");
        int chosenQuest = Random.Range(0, questList.Count);

        currentQuest = new Quest (questList[chosenQuest]);
        questList.RemoveAt(chosenQuest);
          
        

    }

    public void CreateQuest(string name, string description)
    {
      

        if (currentQuest == null)
        {
           
            currentQuest = new Quest(questList[0]);
            questList.RemoveAt(0);
           
        }
        else
        {
                currentQuest = new Quest(name, description);

            for (int i = 0; i < questList.Count; i++)
            {
                if (currentQuest.name == questList[i])
                {
                    questList.RemoveAt(i);
                }
            }
        }
        
    }

    public void TurnInQuest(int photo)
    {
        Debug.Log("Tried to turn in photo");
        bool questFromPool = true;
        int chosenQuest = 500; 
        if (photoManager.photoList[photo].getName() == currentQuest.name)
        {
            finishedQuest.Add(currentQuest);
            
            List<GameObject> creatureList = photoManager.photoList[photo].getCreatures();


            for (int j = 0; j < finishedQuest.Count; j++)

            {
                for (int i = 0; i < creatureList.Count; i++)
                {

                    if (creatureList[i].GetComponent<GenericCreature>().ReturnType() == finishedQuest[j].name)
                    {

                        questFromPool = true;
                        chosenQuest = i;
                        
                    }

                }
            }

            if (!questFromPool)
            {
                
                CreateQuest(creatureList[chosenQuest].GetComponent<GenericCreature>().ReturnType(), creatureList[chosenQuest].GetComponent<GenericCreature>().GetDescription());
                print("Quest from background creature!");
            }
            else
            {
                if (questList.Count > 0)
                {
                    QuestFromPool();
                }
                else
                {
                    print("No more Quests");
                }
                
            }
           
            photoManager.RemovePhoto(photo);
        }
        else
        {
            print("Wrong creature");
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
