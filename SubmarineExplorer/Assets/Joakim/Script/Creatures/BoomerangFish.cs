using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangFish : GenericCreature {

    private string type = "BoomerangFish";
    private string description = "We've heard rumors about a fish that looks and behaves like a boomerang, can you find it for us?";
    

	// Use this for initialization
	override public void Start () {

      
    }
	

    public override string ReturnType()
    {
        return type; 
    }

    public override string GetDescription()
    {
        return description;
    }
}
