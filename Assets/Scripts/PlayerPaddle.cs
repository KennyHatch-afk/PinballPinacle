using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class PlayerPaddle : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed;

    [SerializeField]
    private bool invert;

    private bool isMoving;
    private float dir; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dir = invert ? -1f : 1f;
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = Mouse.current.leftButton.isPressed;
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.AddTorque(-speed * dir);
        }
        else
        {
            // this allows the paddle to return to beginning rotation
            rb.AddTorque(speed * dir);
        }
    }
}
