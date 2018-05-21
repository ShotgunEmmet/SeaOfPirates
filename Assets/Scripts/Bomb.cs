using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public List<Sprite> animationFrames;
    private int currentFrame = 0;

    public float initialDelay = 1f;
    public float animationSpeed = 1f;
    private float timePassed = 0f;

    private Vector3 startSize;

	// Use this for initialization
	void Start () {
        timePassed = -initialDelay;

        startSize = transform.localScale;
	}

    // Update is called once per frame
    void Update() {

        ScaleBomb();

        timePassed += Time.deltaTime;
        if (timePassed > animationSpeed)
        {
            timePassed = 0f;
            currentFrame++;
            if (currentFrame.Equals(animationFrames.Count))
            {
                GameObject.Destroy(gameObject);
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = animationFrames[currentFrame];
            }
        }

    }

    private void ScaleBomb()
    {
        if (timePassed < 0)
        {
            float scale = Mathf.Abs(timePassed % 1);
            if (scale > 0.5f)
            {
                scale = 0.5f - (scale - 0.5f);
            }

            transform.localScale = startSize * (1.25f - scale * 0.5f);
        }
        else
        {
            transform.localScale = startSize;
        }
    }
}
