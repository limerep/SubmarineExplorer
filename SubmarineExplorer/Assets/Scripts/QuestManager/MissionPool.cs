using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionPool : MonoBehaviour {

    //Vilka states som finns för alla uppdrag
	public enum QuestProgress {Not_Available, Available, Accept, Complete, Done}

    //Namn på uppdraget
    public string title;
    //ID nummer för uppdraget
    public int id;
    //Vilket state det nuvarande uppdraget är
    public QuestProgress progress;

    public string description;
    public string hint;
    public string congratuliations;
    public int nextQuest;
    public string questObjective;
    public int questObjectiveCount;
    public int questObjectiveRequirement;
    public int goldReward;
    public int itemReward;


}
