using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterController : MonoBehaviour
{
	Renderer thisRend;
	private float shutterSpeed;
	[SerializeField]
	private bool minValReached;
	private bool runShutter;
	private int maxVal;
	private float minVal;
    private bool testRun;

	void Start()
	{
        testRun = false;
		maxVal = 1;
		minVal = -0.5f;
		minValReached = false;
		runShutter = false;
		shutterSpeed = 1; // set shutter speed
		thisRend = GetComponent<Renderer>(); // Get renderer from 
	}

	// Update is called once per frame
    public void TestRun()
    {
        testRun = true;
    }
	void Update ()
	{
        if (testRun == true && shutterSpeed == 1)
        {
            if (shutterSpeed > minVal) // as long as the alpha is higher than the minimum value
            {
                runShutter = true;
                testRun = false;
            }
        }

        if (runShutter == true)
        {
            shutterSpeed -= 0.2f;
        }

        if (shutterSpeed <= minVal) // As long as the alpha is lower than the minimum value
        {
            minValReached = true;
            runShutter = false;
        }

        if (minValReached == true) // if minimum value is reached
        {
            shutterSpeed += 0.2f;
        }

        if (shutterSpeed >= maxVal)
        {
            minValReached = false;
        }
        thisRend.material.SetFloat("_Cutoff", shutterSpeed); //set alpha
    }


}
    
/*
void Update()
{
	if (Input.GetKey(KeyCode.Space))
	{
		if (shutterSpeed < maxVal)
		{
			shutterSpeed += 0.1f;
		}
	}

	if (shutterSpeed >= maxVal)
	{
		maxValReached = true;
	}

	if (maxValReached == true) // if max value is reached
	{
		shutterSpeed -= 0.1f;
	}

	if (shutterSpeed <= minval)
	{
		maxValReached = false;
	}

	thisRend.material.SetFloat("_Cutoff", shutterSpeed); //set alpha
}
}*/
