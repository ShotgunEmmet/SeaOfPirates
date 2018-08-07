using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour
{
    private Vector2 velocity = Vector2.zero;

    private Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    internal void Move(Vector2 move)
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
            rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
        }
    }
}
