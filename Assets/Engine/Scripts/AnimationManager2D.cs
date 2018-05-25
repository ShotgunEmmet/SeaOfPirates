using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationManager2D : MonoBehaviour {

    public List<Animation2D> animations;
    protected Animation2D currentAnimation;


	private void Start ()
    {
        InstantiateAnimations();
        RunAnimation("Idle");
        GameObject.Find("DebugOutput").GetComponent<Text>().text = "Idle";
    }

    private void InstantiateAnimations()
    {
        GameObject animationContainer = GameObject.Find("AnimationContainer");
        if (animationContainer == null)
        {
            animationContainer = new GameObject("AnimationContainer");
            animationContainer.transform.parent = transform;
        }

        for (int i = 0; i < animations.Count; i++)
        {
            var a = Instantiate(animations[i].gameObject);
            a.transform.parent = animationContainer.transform;
            animations[i] = a.GetComponent<Animation2D>();
        }
    }

    protected void RunAnimation(string animationName)
    {
        foreach (Animation2D animation in animations)
        {
            if (animation.animationName.Equals(animationName))
            {
                if(currentAnimation)
                    currentAnimation.Stop();
                currentAnimation = animation;
                currentAnimation.PlayAnimation(UpdateSprite);
            }
        }
    }

    //This function is only called from a corouteen in Animation2D
    private void UpdateSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
