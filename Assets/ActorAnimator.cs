using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAnimator : MonoBehaviour
{
    private Animator animator;

    private string animatorFacingKey = "Facing";
    private ActorFacing facing = ActorFacing.TOP;
    public ActorFacing Facing {
        get {return facing;} 
        set {
            facing = value;
            switch(facing) {
                case ActorFacing.TOP: 
                    UpdateField(animatorFacingKey, 0);
                    break;
                case ActorFacing.LEFT: 
                    UpdateField(animatorFacingKey, 1);
                    break;
                case ActorFacing.RIGHT: 
                    UpdateField(animatorFacingKey, 2);
                    break;
                case ActorFacing.BOTTOM: 
                    UpdateField(animatorFacingKey, 3);
                    break;
            }
        }
    }
    
    private string animatorIdleKey = "Idle";
    private bool idle = true;
    public bool Idle {
        get {return idle;} 
        set {
            idle = value;
            UpdateField(animatorIdleKey, value);
        }
    }
    
    private bool walking = false;
    public bool Walking {
        get {return walking;} 
        set {
            walking = value;
            // TODO: Update the animator component
        }
    }
    
    private bool meleeWindup = false;
    public bool MeleeWindup {
        get {return meleeWindup;} 
        set {
            meleeWindup = value;
            // TODO: Update the animator component
        }
    }

    private bool meleeAttacking = false;
    public bool MeleeAttacking {
        get {return meleeAttacking;} 
        set {
            meleeAttacking = value;
            // TODO: Update the animator component
        }
    }

    private bool injured = false;
    public bool Injured {
        get {return injured;} 
        set {
            injured = value;
            // TODO: Update the animator component
        }
    }

    private bool invincibilityFramesActive = false;
    public bool InvincibilityFramesActive {
        get {return invincibilityFramesActive;} 
        set {
            invincibilityFramesActive = value;
            // TODO: Update the animator component
        }
    }

    private bool knockbackActive = false;
    public bool KnockbackActive {
        get {return knockbackActive;} 
        set {
            knockbackActive = value;
            // TODO: Update the animator component
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if(animator == null) {
            Debug.Log("No animator on " + gameObject.name + " - animation will not work");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateField(string key, bool value) {
        if(animator != null) {
            animator.SetBool(key, value);
        }
    }
    
    void UpdateField(string key, float value) {
        if(animator != null) {
            animator.SetFloat(key, value);
        }
    }
 
    void UpdateField(string key, int value) {
        if(animator != null) {
            animator.SetInteger(key, value);
        }
    }
}
