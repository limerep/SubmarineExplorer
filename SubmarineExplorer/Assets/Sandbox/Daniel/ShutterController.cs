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

	void Start()
	{
		maxVal = 1;
		minVal = -0.5f;
		minValReached = false;
		runShutter = false;
		shutterSpeed = 1; // set shutter speed
		thisRend = GetComponent<Renderer>(); // Get renderer from object

	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey(KeyCode.Space))
		{
			if (shutterSpeed > minVal) // as long as the alpha is higher than the minimum value
			{
				runShutter = true;
			}
		}

		if (runShutter == true)
		{
			shutterSpeed -= 0.1f;
		}

		if (shutterSpeed <= minVal) // As long as the alpha is lower than the minimum value
		{
			minValReached = true;
			runShutter = false;
		}

		if (minValReached == true) // if minimum value is reached
		{
			shutterSpeed += 0.1f;
		}

		if(shutterSpeed >= maxVal)
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
   