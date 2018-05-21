using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWake : MonoBehaviour {

    public enum AnimationType {once, loop, destroy};
    public AnimationType animationType = AnimationType.loop;

    public float animationSpeed = 1f;
    private float timePassed = 0f;

    public enum DirectionTypes { one = 0, four, eight, flipX };
    public DirectionTypes Directions = DirectionTypes.one;
    public List<Sprite> NA, L, TL, T, TR, R, BR, B, BL;
    private List<Sprite> direction = new List<Sprite>();
    private int currentFrame = 0;
    
    private void Update () {
        if (direction.Capacity > 0)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= animationSpeed)
            {
                timePassed -= animationSpeed;
                currentFrame++;
                if (direction.Capacity > currentFrame)
                {
                    SetSprite();
                }
                else if (animationType == AnimationType.loop)
                {
                    currentFrame = 0;
                    SetSprite();
                }
                else if (animationType == AnimationType.destroy)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void SetDirection(float angle)
    {

        if (Directions == DirectionTypes.eight)
        {
            float turn = 22.5f;

            if (angle < turn)
                direction = B;
            else if (angle < (turn += 45f))
                direction = BR;
            else if (angle < (turn += 45f))
                direction = R;
            else if (angle < (turn += 45f))
                direction = TR;
            else if (angle < (turn += 45f))
                direction = T;
            else if (angle < (turn += 45f))
                direction = TL;
            else if (angle < (turn += 45f))
                direction = L;
            else if (angle < (turn += 45f))
                direction = BL;
            else
                direction = B;
        }
        else if (Directions == DirectionTypes.four)
        {
            float turn = 45f;

            if (angle < turn)
                direction = B;
            else if (angle < (turn += 90f))
                direction = R;
            else if (angle < (turn += 90f))
                direction = T;
            else if (angle < (turn += 90f))
                direction = L;
            else
                direction = B;
        }
        else if (Directions == DirectionTypes.one)
        {
            direction = NA;
        }
        else if (Directions == DirectionTypes.flipX)
        {
            direction = NA;
            if(angle > 0 && angle < 180)
            {
                (gameObject.GetComponent("SpriteRenderer") as SpriteRenderer).flipX = true;
            }
        }

        SetSprite();
    }

    private void SetSprite()
    {
        (this.gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer).sprite = direction[currentFrame];
    }
}
