using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScissorAI : MonoBehaviour
{
    public Transform point1;
    public Transform point2;
    public float speed = 1f;
    public float arrivalThreshold = 0.05f;

    private Transform currentTarget;

    void Start()
    {
        currentTarget = point1;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, currentTarget.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTarget.position) < arrivalThreshold) {
            currentTarget = currentTarget == point1 ? point2 : point1;
            transform.Rotate(0, 0, 180f);
        }

    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
        {
            //Game over condition
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }    
    }
}

