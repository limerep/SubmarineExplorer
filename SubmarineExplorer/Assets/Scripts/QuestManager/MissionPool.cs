using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionPool {

    //Vilka states som finns för alla uppdrag
	public enum QuestProgress {Not_Available, Available, Accept, Complete, Done}

    //Namn på uppdraget
    public string title;
    //ID nummer för uppdraget
    public int id;
    //Vilket state det nuvarande uppdraget är
    public QuestProgress progress;
    //Vad uppdraget innehåller
    public string description;
    //
    public string hint;
    public string congratulations;
    public string summary;
    public int nextQuest;

    public string questObjective;
    public int questObjectiveCount;
    public int questObjectiveRequirement;

    //Vad för rewards man får
    public int goldReward;
    public string itemReward;


}
