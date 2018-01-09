using UnityEngine;

public class Spin : MonoBehaviour {

    public float rotateXspeed = 0f;
    public float rotateYspeed = 90f;
    public float rotateZspeed = 0f;

    public bool local = false;

	void Update () {
        if (local == false)
        {
            transform.Rotate(rotateXspeed * Time.deltaTime, rotateYspeed * Time.deltaTime, rotateZspeed * Time.deltaTime, Space.World);
        }

        if (local == true)
        {
            transform.Rotate(rotateXspeed * Time.deltaTime, rotateYspeed * Time.deltaTime, rotateZspeed * Time.deltaTime);
        }

    }
}

