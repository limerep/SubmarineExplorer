using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGlobalFlock : MonoBehaviour {

    public BoxGlobalFlock myFlock;
    public GameObject fishPrefab;

    [SerializeField]
    private bool x5, x10, x15, x20, x25, x30; 

    static int numFish = 10;
    public GameObject[] allFish;
    public Vector3 goalPos;
    // Set the size of the bounding box to keep the fish within.
    // Actual side length will be twice the values given here
    public Vector3 swimLimits = new Vector3(5, 5, 5);

    private void Awake()
    {
        if (x5)
            numFish = 5;
        else if (x10)
            numFish = 10;
        else if (x15)
            numFish = 15;
        else if (x20)
            numFish = 20;
        else if (x25)
            numFish = 25;
        else if (x30)
            numFish = 30;
        else
            Debug.Log("NUMFISH NOT SET IN BoxGlobalFlock.cs");

        allFish = new GameObject[numFish];
    }


    public void FishSpeed(float speedMult)
    {
        Debug.Log(speedMult);
        for(int i= 0; i < numFish; i++)
        {
            allFish[i].GetComponent<BoxFlock>().speed = speedMult;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(swimLimits.x * 2, swimLimits.y * 2, swimLimits.z * 2));
        Gizmos.color = new Color(0, 1, 0, 1);
        Gizmos.DrawSphere(goalPos, 0.5f);
    }

    private void Start ()
    {
        myFlock = this;
        goalPos = this.transform.position;

        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                                                Random.Range(-swimLimits.y, swimLimits.y),
                                                                Random.Range(-swimLimits.z, swimLimits.z));

            var go = Instantiate(fishPrefab, pos, Quaternion.identity);
            //go.transform.LookAt(goalPos);
            allFish[i] = go;
            go.transform.SetParent(gameObject.transform.parent);
            allFish[i].GetComponent<BoxFlock>().myManager = this;
        }
	}

    private void Update()
    {
        if(Random.Range(0, 10000) < 50)
        {
            goalPos = this.transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                                            Random.Range(-swimLimits.y, swimLimits.y),
                                                            Random.Range(-swimLimits.z, swimLimits.z));
        }
    }
}
