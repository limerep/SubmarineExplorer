using UnityEngine;

public class FollowObject : MonoBehaviour
{

    public Transform objectToFollow;
    [SerializeField]
    Vector3 offset = new Vector3(0, 0, 0);


    float smoothTime = 15f, maxSpeed = 8f;
    Vector3 velocity = Vector3.zero;


    public void SetObjectToFollow(GameObject objectToFollow)
    {
        this.objectToFollow = objectToFollow.transform;
    }

    void LateUpdate()
    {
        if (objectToFollow != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, objectToFollow.position - offset, ref velocity, smoothTime * Time.deltaTime, maxSpeed);
        }
        else
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref velocity, smoothTime * Time.deltaTime, maxSpeed);
        }

    }

}
