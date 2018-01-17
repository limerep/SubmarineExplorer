using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBox : MonoBehaviour {

    public GlobalFishBox boundingBox;
    public float speed = 0.01f;
    public float rotationSpeed = 4.0f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float neighbourDistance = 5.0f;

    bool turning = false;

    public float speedMult = 1;

    private void Start() {
        speed = Random.Range(speed * 0.5f, speed);
    }

    private void Update() {
        // Determine the bounding box of the manager cube
        Bounds b = new Bounds(boundingBox.transform.position, boundingBox.swimLimits * 2);
        
        if (!b.Contains(transform.position)) {
            Vector3 direction = boundingBox.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                 Quaternion.LookRotation(direction),
                                 rotationSpeed * Time.deltaTime);
            speed = Random.Range(.5f, 1) * speedMult;
        } else {
            if (Random.Range(0, 5) < 1) {
                ApplyRules();
            }
        }

        transform.Translate(0, 0, Time.deltaTime * speed * speedMult);
    }

    private void ApplyRules() {
        GameObject[] gos;
        gos = boundingBox.allFish;

        Vector3 vcenter = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.1f;

        Vector3 goalPos = boundingBox.goalPos;

        float dist;

        int groupSize = 0;
        foreach (GameObject go in gos) {
            if (go != gameObject) {
                dist = Vector3.Distance(go.transform.position, transform.position);
                if (dist <= neighbourDistance) {
                    vcenter += go.transform.position;
                    groupSize++;

                    if (dist < 2.0f) {
                        vavoid = vavoid + (transform.position - go.transform.position);
                    }

                    FishBox anotherFlock = go.GetComponent<FishBox>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0) {
            vcenter = vcenter / groupSize + (goalPos - transform.position);
            speed = gSpeed / groupSize * speedMult;

            Vector3 direction = (vcenter + vavoid) - transform.position;
            if (direction != Vector3.zero) {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction.normalized),
                                                      rotationSpeed * Time.deltaTime);
            }
        }
    }
}
