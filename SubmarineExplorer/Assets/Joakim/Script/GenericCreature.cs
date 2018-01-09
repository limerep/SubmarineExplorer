using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCreature : MonoBehaviour {

   


	// Use this for initialization
	virtual public void Start () {

        
	}
	

    virtual public string ReturnType()
    {
        return ""; 
    }

    virtual public string GetDescription()
    {
        return "";
    }
}
