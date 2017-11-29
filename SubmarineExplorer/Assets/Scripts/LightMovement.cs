using UnityEngine;
using System.Collections;

public class LightMovement : MonoBehaviour
{

    float duration = 20.0F;
    public Color color0;
    public Color color1;
    Light lt;

    [SerializeField]
    float minIntense, maxIntense;

    public bool rotateAroundY;
    bool rotate;
    float rotSpeed = 0.001f;

    void Start()
    {
        lt = GetComponent<Light>();
    }
    void Update()
    {
        //Lerp between colors
        float t = Mathf.PingPong(Time.time, duration) / duration;
        lt.color = Color.Lerp(color0, color1, t);

        lt.intensity = Mathf.Lerp(minIntense, maxIntense, t);
        Debug.Log(t);

        if(rotateAroundY)
        {
            if (t > 0.5)
                rotate = true;
            if (t < 0.5)
                rotate = false;

            if (rotate)
                transform.Rotate(0, rotSpeed, 0);
            if (!rotate)
                transform.Rotate(0, -rotSpeed, 0);
        }
    }
}