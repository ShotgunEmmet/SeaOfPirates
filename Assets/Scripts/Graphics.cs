using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graphics : MonoBehaviour {

    public enum DirectionTypes { four = 0, eight };
    public DirectionTypes Directions = DirectionTypes.four;
    public List<Sprite> L, TL, T, TR, R, BR, B, BL;
    private List<Sprite> direction = new List<Sprite>();

    public int numberOfAnimationFrames = 1;
    public float animationSpeed = 1f;
    private int animationTile = 0;
    private float timePassed = 0f;

    public GameObject trail;
    private float trailSize;
    private Vector3 oldPos;



    // Use this for initialization
    void Start()
    {
        if (trail)
        {
            trailSize = trail.transform.localScale.x;
            oldPos = transform.position;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        timePassed += Time.deltaTime;
    }

    public void SetSprite()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = direction[animationTile];
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

    public void ResetLook()
    {
        direction = B;
        animationTile = 0;
        SetSprite();
    }

    public void Move(float angle)
    {

        CreateTrail(angle);

        timePassed += Time.deltaTime;
        if (timePassed > (animationSpeed / numberOfAnimationFrames))
        {
            timePassed = 0f;
            animationTile++;
            animationTile %= numberOfAnimationFrames;
        }

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
        else
        {
            animationTile = 0;
        }

        this.SetSprite();
    }
}
