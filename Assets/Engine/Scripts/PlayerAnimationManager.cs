using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : AnimationManager2D
{

    private enum Direction { none, up, down, left, right };
    private Direction oldDirection = Direction.none;

    public GameObject trail;
    private float trailSize;
    private Vector3 oldPos;


    void Start()
    {
        if (trail)
        {
            trailSize = trail.transform.localScale.x;
            oldPos = transform.position;
        }
    }

    void Update()
    {
        
        base.Update();
    }

    public void Move(float angle)
    {
        CreateTrail(angle);
        
        float turn = 45f;

        if (angle < turn)
            SetDirection(Direction.down);
        else if (angle < (turn += 90f))
            SetDirection(Direction.right);
        else if (angle < (turn += 90f))
            SetDirection(Direction.up);
        else if (angle < (turn += 90f))
            SetDirection(Direction.left);
        else
            SetDirection(Direction.down);


    }

    private void CreateTrail(float angle)
    {
        if (trail)
        {
            if ((oldPos - transform.position).magnitude > trailSize)
            {
                oldPos = transform.position;

                var newProjectile = Instantiate(trail);
                newProjectile.transform.position += oldPos;
                newProjectile.GetComponent<WaterWake>().SetDirection(angle);

            }
        }
    }

    private void SetDirection(Direction newDirection)
    {
        if (!oldDirection.Equals(newDirection))
        {
            oldDirection = newDirection;
            if (oldDirection.Equals(Direction.up))
                RunAnimation("up");
            else if (oldDirection.Equals(Direction.down))
                RunAnimation("down");
            else if (oldDirection.Equals(Direction.left))
                RunAnimation("left");
            else if (oldDirection.Equals(Direction.right))
                RunAnimation("right");
        }
    }
}
