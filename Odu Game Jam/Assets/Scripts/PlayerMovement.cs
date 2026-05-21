using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Vector2 mousePos;
    public GameObject mouseCheck;

    void Update() {
        mousePos = Input.mousePosition;
        mouseCheck.transform.position = mousePos;
    }

    
}
