using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFishBox : MonoBehaviour {
    
    public FishPropterties fishProps;
    
    private int numFish = 1;
    
    [HideInInspector]
    public GameObject[] allFish;
    public Vector3 goalPos { get; private set; }
    // Set the size of the bounding box to keep the fish within.
    // Actual side length will be twice the values given here
    public Vector3 swimLimits = new Vector3(5, 5, 5);

    private void Awake() {

        if (fishProps.Family == FishFamily.School) {
            numFish = Random.Range(fishProps.MinAmount, fishProps.MaxAmount);
        } else {
            numFish = 1;
        }

        allFish = new GameObject[numFish];
    }

    private void Start() {
        goalPos = transform.position;

        GetComponent<NavMeshSourceTag>().enabled = false;

        for (int i = 0; i < numFish; i++) {
            Vector3 pos = transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                                           Random.Range(-swimLimits.y, swimLimits.y),
                                                           Random.Range(-swimLimits.z, swimLimits.z));

            var go = new GameObject();
            go.transform.position = pos;
            FishBox fish = go.AddComponent<FishBox>();
            MeshFilter fishMesh = go.AddComponent<MeshFilter>();
            fishMesh.sharedMesh = fishProps.Model;
            MeshRenderer mRend = go.AddComponent<MeshRenderer>();
            mRend.material = fishProps.FishMaterial;
            fish.speed = fishProps.SwimSpeed;
            fish.rotationSpeed = fishProps.TurnSpeed;
            fish.boundingBox = this;
            allFish[i] = go;
            go.transform.SetParent(gameObject.transform);
        }
        GetComponent<NavMeshSourceTag>().enabled = true;

        StartCoroutine("RandomizeGoalPosition");
    }

    private void OnDrawGizmosSelected() {
        // Draw swimlimit box
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(swimLimits.x * 2, swimLimits.y * 2, swimLimits.z * 2));
        Gizmos.color = new Color(0, 1, 0, 1);
        Gizmos.DrawSphere(goalPos, 0.5f);
    }

    IEnumerator RandomizeGoalPosition() {
        // Gives fishes a new goal position every 1 to n seconds.
        while (gameObject.activeSelf) {
            goalPos = transform.position + new Vector3(Random.Range(-swimLimits.x, swimLimits.x),
                                                       Random.Range(-swimLimits.y, swimLimits.y),
                                                       Random.Range(-swimLimits.z, swimLimits.z));

            yield return new WaitForSeconds(Random.Range(1.0f, fishProps.ReactionTime));
        }
    }

    public void FishSpeed(float speedMult) {
        Debug.Log(speedMult);
        for (int i = 0; i < numFish; i++) {
            allFish[i].GetComponent<FishBox>().speed = speedMult;
        }
    }
}
