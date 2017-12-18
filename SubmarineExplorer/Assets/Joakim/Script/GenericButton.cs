using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericButton : MonoBehaviour {

    public virtual void ButtonPressed(GameObject character)
    {

        print("Bla");
    }
    public virtual void VrButtonPress(GameObject character)
    {
        print("Vr Yo!");
    }
}
