using System;
using UnityEngine;

public class ScissorAI : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float speed = 1f;



    void Update()
    {
        if (point1.position.x < point2.position.x)
        {
            transform.position = Vector3.Lerp(transform.position, point1.position, Time.deltaTime * speed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, point2.position, Time.deltaTime * speed);
        }
    }
}

