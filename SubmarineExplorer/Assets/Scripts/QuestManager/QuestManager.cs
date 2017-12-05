using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public static QuestManager questManager;

    //Master quets listan
    public List <MissionPool> questList = new List <MissionPool>(); 
    //Nuvarande quest listan
    public List <MissionPool> currentQuest = new List<MissionPool>();

    //privata varabler för QuestObject

    void Awake()
    {
        if (questManager == null)
        {
            questManager = this;
        }

        else if (questManager != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    //Acceptera questet

    public void AcceptQuest(int questID)
    {
        for(int i = 0; i <questList.Count;i++)
        {
            if (questList[i].id == questID && questList[i].progress == MissionPool.QuestProgress.Available)
            {
                currentQuest.Add(questList[i]);
                questList[i].progress = MissionPool.QuestProgress.Accept;
            }
        }
    }

    //Ge upp questet

    public void GiveUpQuest(int questID)
    {
        for(int i = 0; i < currentQuest.Count; i++)
        {
            if(currentQuest[i].id == questID && currentQuest[i].progress == MissionPool.QuestProgress.Accept)
            {
                currentQuest[i].progress = MissionPool.QuestProgress.Available;
                currentQuest[i].questObjectiveCount = 0;
                currentQuest.Remove(currentQuest[i]);
            }
        }
    }

    //Complete quest

    public void CompleteQuest(int questID)
    {
        for (int i = 0; i < currentQuest.Count; i++)
        {
            if(currentQuest[i].id = questID && currentQuest[i].progress == MissionPool.QuestProgress.Complete)
            {
                currentQuest[i].progress = MissionPool.QuestProgress.Done;
                currentQuest.Remove(currentQuest[i]);
            }
        }
    }



    //Lägg till items (Kan vara att fotografera eller hitta något)
    public void AddQuestItem(string questObjective, int itemAmount)
    {
        for (int i = 0; i < currentQuest.Count; i++)
        {
            if(currentQuest[i].questObjective == questObjective && currentQuest[i].progress == MissionPool.QuestProgress.Accept)
            {
                currentQuest[i].questObjectiveCount += itemAmount;
            }

            if(currentQuest[i].questObjectiveCount >= currentQuest[i].questObjectiveRequirement && currentQuest[i].progress == MissionPool.QuestProgress.Accept)
            {
                currentQuest[i].progress = MissionPool.QuestProgress.Complete;
            }
        }
    }
    
    //Plocka bort items

    //Bools
    public bool RequestAvailableQuest(int questID)
    {
        for(int i = 0; i < questList.Count; i++)
        {
            if(questList[i].id == questID && questList[i].progress == MissionPool.QuestProgress.Available)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestAcceptedQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == MissionPool.QuestProgress.Accept)
            {
                return true;
            }
        }
        return false;
    }

    public bool RequestCompleteQuest(int questID)
    {
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].id == questID && questList[i].progress == MissionPool.QuestProgress.Complete)
            {
                return true;
            }
        }
        return false;
    }
}
