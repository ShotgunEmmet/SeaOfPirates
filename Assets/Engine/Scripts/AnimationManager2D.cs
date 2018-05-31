using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationManager2D : MonoBehaviour
{

    public List<Animation2D> animations;
    protected Animation2D currentAnimation;
    
    protected enum AnimationState { stopped, paused, playing };
    protected AnimationState animationState;
    
    private int currentFrame;
    private float timePassed;


    protected void Update()
    {
        if (animationState.Equals(AnimationState.playing))
        {
            timePassed += Time.deltaTime;
            if (timePassed >= currentAnimation.animationSpeed)
            {
                timePassed -= currentAnimation.animationSpeed;

                currentFrame++;
                currentFrame %= currentAnimation.animationFrames.Count;

                UpdateFrame();
            }
        }
    }

    protected void RunAnimation(string animationName)
    {
        ResetAnimation();
        foreach (Animation2D animation in animations)
        {
            if (animation.animationName.Equals(animationName, System.StringComparison.InvariantCultureIgnoreCase))
            {
                if (currentAnimation)
                    Stop();
                currentAnimation = animation;
                animationState = AnimationState.playing;
                UpdateFrame();
            }
        }
    }

    protected void Play()
    {
        animationState = AnimationState.playing;
    }

    protected void Pause()
    {
        timePassed = currentAnimation.animationSpeed;
        animationState = AnimationState.paused;
    }

    public void Stop()
    {
        ResetAnimation();
        animationState = AnimationState.stopped;
    }

    private void ResetAnimation()
    {
        timePassed = 0f;
        currentFrame = 0;
        animationState = AnimationState.stopped;
    }

    private void UpdateFrame()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = currentAnimation.animationFrames[currentFrame];
    }

}
