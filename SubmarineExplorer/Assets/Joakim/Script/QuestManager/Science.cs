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
        
	}

    //Creates a quest from the mission pool
    public void QuestFromPool()
    {
        print("Quest From Pool");
        int chosenQuest = Random.Range(0, questList.Count);

        currentQuest = new Quest (questList[chosenQuest]);
        questList.RemoveAt(chosenQuest);
          
        

    }

    //Creates a quest from a creature in the background of the photo
    public void CreateQuest(string name, string description)
    {
      
        //If the quest is null, create a new questList
        if (currentQuest == null)
        {
           
            currentQuest = new Quest(questList[0]);
            questList.RemoveAt(0);
            questText.text = currentQuest.name; 
           
        }
        //Use the description and name from a creature in the background to create a new quest
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
    //Function for turning in quests and 
    public void TurnInQuest(int photo)
    {
        Debug.Log("Tried to turn in photo");
        bool questFromPool = true;
        int chosenQuest = 500; 

        if (photoManager.photoList[photo].getName() == currentQuest.name)
        {
            finishedQuest.Add(currentQuest);
            
            List<GameObject> creatureList = photoManager.photoList[photo].getCreatures();


            //Remove creatures that have the same name as the current quest 


            for (int j = 0; j < finishedQuest.Count; j ++)
                 
            {
                
                for (int i = 0; i < creatureList.Count; i++)
                {
                    string creatureName = creatureList[i].GetComponent<GenericCreature>().ReturnType();

                    if (creatureName == finishedQuest[j].name)
                    {

                        creatureList.RemoveAt(i);

                        if (i > 0)
                        {
                            i--;
                        }
                      
                    }
                }

                if (creatureList.Count == 0)
                {
                    break;
                }
               
            }

            if (creatureList.Count > 0)
            {
                questFromPool = false;
                chosenQuest = 0;
            }

            if (!questFromPool)
            {
                
                CreateQuest(creatureList[chosenQuest].GetComponent<GenericCreature>().ReturnType(), creatureList[chosenQuest].GetComponent<GenericCreature>().GetDescription());
                print("Quest from background creature!");
                questText.text = currentQuest.name;
            }
            else
            {
                if (questList.Count > 0)
                {
                    QuestFromPool();
                    questText.text = currentQuest.name;
                }
                else
                {
                    print("No more Quests");
                    questText.text = "No More Quests";
                    currentQuest = new Quest("");
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
        //Cursor.lockState = CursorLockMode.None;
    }
}
