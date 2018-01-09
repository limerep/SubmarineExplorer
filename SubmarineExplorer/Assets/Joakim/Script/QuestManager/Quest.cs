using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest {

    public string description;
    public string name;

    public Quest(string fishName, string creatureDescription)
    {

        name = fishName;
        description = creatureDescription ; 
    }

    public Quest(string fishName)
    {
        name = fishName;

        GenericCreature[] creatures = GameObject.FindObjectsOfType<GenericCreature>();

        for (int i = 0; i < creatures.Length; i++)
        {
            if (name == creatures[i].ReturnType())
            {
                description = creatures[i].GetDescription();
                break; 
            }
        }

    }



}
