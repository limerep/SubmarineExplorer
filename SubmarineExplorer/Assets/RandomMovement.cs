using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {

    public float speed, rotSpeed;
    public float randomX, randomY, randomZ;
    public float minWaitTime;
    public float maxWaitTime;
    private Vector3 currentRandomPos;


    Transform target;
    GameObject spawnObject;

    void Start()
    {
        PickPosition();
    }

    void Update()
    {
        if(spawnObject)
        {
            Vector3 targetDir = target.position - transform.position;
            float step = rotSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            Debug.DrawRay(transform.position, newDir, Color.red);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
       
    }

    void PickPosition()
    {
        currentRandomPos = new Vector3(Random.Range(-randomX, randomX), Random.Range(-randomY, randomY), Random.Range(-randomZ, randomZ));
        spawnObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        spawnObject.transform.position = currentRandomPos;
        spawnObject.SetActive(false);


        target = spawnObject.transform;
        StartCoroutine(MoveToRandomPos());

        Destroy(spawnObject, 10f);
    }

    IEnumerator MoveToRandomPos()
    {
        float i = 0.0f;
        float rate = 1.0f / speed;
        Vector3 currentPos = transform.position;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(currentPos, currentRandomPos, i);
            yield return null;
        }

        float randomFloat = Random.Range(0.0f, 1.0f); // Create %50 chance to wait
        if (randomFloat < 0.5f)
            StartCoroutine(WaitForSomeTime());
        else
            PickPosition();
    }

    IEnumerator WaitForSomeTime()
    {
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        PickPosition();
    }
}
