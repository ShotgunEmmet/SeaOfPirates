using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation2D : MonoBehaviour {

    public string animationName;

    public float animationSpeed = 0.2f;
    private float timePassed;

    public List<Sprite> animationFrames;
    private int currentFrame;

    public enum AnimationState { stopped, paused, playing};
    private AnimationState animationState;

    public delegate void CallbackUpdateSprite(Sprite animationFrame);
    public CallbackUpdateSprite updateSprite;


    void Start () {
        ResetAnimation();
	}

    private void Update ()
    {
        if (animationState.Equals(AnimationState.playing))
        {
            timePassed += Time.deltaTime;
            if (timePassed >= animationSpeed)
            {
                timePassed -= animationSpeed;

                currentFrame++;
                currentFrame %= animationFrames.Count;

                UpdateFrame();
            }
        }
    }

    public void PlayAnimation(CallbackUpdateSprite _updateSprite)
    {
        updateSprite = _updateSprite;

        animationState = AnimationState.playing;
        UpdateFrame();

    }

    public void Play()
    {
        animationState = AnimationState.playing;
    }

    public void Pause()
    {
        timePassed = animationSpeed;
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
        updateSprite(animationFrames[currentFrame]);
    }

    public AnimationState State()
    {
        return animationState;
    }
    
}
