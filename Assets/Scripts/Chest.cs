using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, ITriggerable {

    public enum AnimationType{ once, loop, destroy};
    public AnimationType animationType = AnimationType.once;

    public List<Sprite> frames;
    public float animationSpeed = 1f;

    private bool triggered = false;
    private int animationFrame = 0;
    private float timePassed = 0f;

    public AudioClip soundfx;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            timePassed += Time.deltaTime;
            if (timePassed > animationSpeed)
            {
                timePassed -= animationSpeed;
                animationFrame++;

                if (animationFrame >= frames.Count)
                {
                    if (animationType.Equals(AnimationType.once))
                    {
                        triggered = false;
                    }
                    else if (animationType.Equals(AnimationType.loop))
                    {
                        animationFrame = 0;
                        (gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer).sprite = frames[animationFrame];
                    }
                    else if (animationType.Equals(AnimationType.destroy))
                    {
                        GameObject.Destroy(gameObject);
                    }
                }
                else
                {
                    (gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer).sprite = frames[animationFrame];
                }
                
            }
        }
    }

    public void Trigger()
    {
        triggered = true;
        
        if(soundfx)
        {
            audioSource.PlayOneShot(soundfx, 0.5f);
        }
        else
        {
            Debug.Log("nope");
        }
    }
}
