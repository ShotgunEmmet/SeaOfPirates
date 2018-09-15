using System;
using Application;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour,IMotor
{
    private Vector2 velocity = Vector2.zero;

    private Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 move)
    {
        velocity = move;
    }

    private void FixedUpdate()
    {
        PerformMovement();
    }

    private void PerformMovement()
    {
        if (velocity != Vector2.zero)
        {
            rigidbody.velocity = velocity;
        }
    }
}
