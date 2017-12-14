using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangFish : GenericCreature {

    string type = "BoomerangFish"; 


	// Use this for initialization
	override public void Start () {

        type = "BoomerangFish";
	}
	

    public override string ReturnType()
    {
        return type; 
    }
}
