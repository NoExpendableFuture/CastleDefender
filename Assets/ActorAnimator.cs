using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 *    Frame based animator
 *   IMPORTANT NOTE: To use this animator, standardise idle, walk, and attack animation names in this format: direction_statename, ie. Top_Idle or Left_Melee
 *                   statename is as per the actor state names
 */
public class ActorAnimator : MonoBehaviour
{
    private Animator animator;
    private Actor actor;
    private string currentAnimation = "Top_Idle";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        actor = GetComponent<Actor>();

        if(animator == null) {
            Debug.Log("No animator on " + gameObject.name + " - animation will not work");
        } else {
            animator.Play(currentAnimation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
    }

    void UpdateAnimation() {
        if(animator == null || actor == null) {
            return;
        }
        
        string animName = "";
        List<string> directionalAnimations = new List<string>
            { ActorStateName.IDLE, ActorStateName.MELEE, ActorStateName.MELEE_WINDUP, ActorStateName.WALK };

        if(directionalAnimations.Contains(actor.ActorState.StateName())) {
            switch(actor.Facing) {
                case ActorFacing.TOP: 
                    animName += "Top_";
                    break;
                case ActorFacing.LEFT: 
                    animName += "Left_";
                    break;
                case ActorFacing.RIGHT: 
                    animName += "Right_";
                    break;
                case ActorFacing.BOTTOM: 
                    animName += "Bottom_";
                    break;
            }

        }

        animName += actor.ActorState.StateName();

        if(currentAnimation != animName) {
            // Debug.Log(gameObject.name + ": New animation state " + animName);
            currentAnimation = animName;
            animator.Play(animName);
        }

    }
    
}
